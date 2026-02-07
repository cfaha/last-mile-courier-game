package com.cfaha.lastmile

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent { App() }
    }
}

enum class Screen { Main, Planning, Delivery, Result, Shop }

@Composable
fun App() {
    val ctx = androidx.compose.ui.platform.LocalContext.current
    val levelsJson = ctx.assets.open("levels.json").bufferedReader().readText()
    val eventsJson = ctx.assets.open("events.json").bufferedReader().readText()
    val weightsJson = ctx.assets.open("order_weights.json").bufferedReader().readText()
    val shopJson = ctx.assets.open("shop.json").bufferedReader().readText()

    val levels = remember { JsonParser.parseLevels(levelsJson) }
    val weights = remember { JsonParser.parseOrderWeights(weightsJson) }
    val events = remember { JsonParser.parseEventWeights(eventsJson) }
    val shopItems = remember { JsonParser.parseShop(shopJson) }

    val engine = remember { GameEngine(levels, events, weights) }
    var screen by remember { mutableStateOf(Screen.Main) }
    var level by remember { mutableStateOf(engine.loadLevel(engine.currentLevelId)) }
    var showEvent by remember { mutableStateOf(false) }
    var eventText by remember { mutableStateOf("") }
    var result by remember { mutableStateOf<GameResult?>(null) }

    MaterialTheme {
        Column(modifier = Modifier.fillMaxSize().background(Color(0xFFF5F5F5)).padding(16.dp)) {
            Text("快递员：最后一公里", style = MaterialTheme.typography.headlineMedium, fontWeight = FontWeight.Bold)
            Spacer(modifier = Modifier.height(8.dp))

            when (screen) {
                Screen.Main -> {
                    Text("关卡：${engine.currentLevelId}")
                    Text("金币：${engine.coins}")
                    Spacer(modifier = Modifier.height(12.dp))
                    Button(onClick = {
                        level = engine.loadLevel(engine.currentLevelId)
                        engine.generateOrders(level)
                        screen = Screen.Planning
                    }) { Text("开始") }
                    Spacer(modifier = Modifier.height(8.dp))
                    Button(onClick = { screen = Screen.Shop }) { Text("商店") }
                }
                Screen.Planning -> {
                    Text("规划页 - 订单列表")
                    LazyColumn(modifier = Modifier.weight(1f)) {
                        items(engine.orders) { o ->
                            Card(Modifier.fillMaxWidth().padding(4.dp)) {
                                Row(Modifier.padding(8.dp), verticalAlignment = Alignment.CenterVertically) {
                                    Text("#${o.id} ${o.type}")
                                    Spacer(Modifier.width(8.dp))
                                    Text("${o.distanceKm}km / ${o.timeLimit}s")
                                    Spacer(Modifier.weight(1f))
                                    Text(if (o.delivered) "已送" else "待送", color = if (o.delivered) Color.Green else Color.Gray)
                                }
                            }
                        }
                    }
                    Button(onClick = { screen = Screen.Delivery }) { Text("开始配送") }
                }
                Screen.Delivery -> {
                    Text("配送中")
                    Spacer(modifier = Modifier.height(8.dp))
                    Button(onClick = {
                        val (order, done) = engine.deliverNext(engine.efficiency)
                        if (order == null) return@Button
                        if (Math.random() < level.eventChance || level.forcedEvent.isNotEmpty()) {
                            val e = if (level.forcedEvent.isNotEmpty()) level.forcedEvent else engine.pickEvent(level)
                            eventText = when (e) {
                                "Gate" -> "门禁：等待或绕行"
                                "Rain" -> "暴雨：速度下降"
                                else -> "管制：需要绕行"
                            }
                            showEvent = true
                        }
                        if (done) {
                            val score = engine.calcScore()
                            val coins = engine.calcReward()
                            engine.coins += coins
                            result = GameResult(
                                delivered = engine.orders.count { it.delivered },
                                total = engine.orders.size,
                                score = score,
                                onTimeRate = engine.onTimeRate,
                                efficiency = engine.efficiency,
                                coinsEarned = coins,
                                failed = engine.isFailed()
                            )
                            screen = Screen.Result
                        }
                    }) { Text("送下一单") }
                }
                Screen.Result -> {
                    val r = result
                    if (r != null) {
                        Text("结算：${r.delivered}/${r.total} 评分 ${String.format("%.2f", r.score)}")
                        Text("准时率 ${(r.onTimeRate * 100).toInt()}% 效率 ${(r.efficiency * 100).toInt()}%")
                        Text("金币 +${r.coinsEarned}，总计 ${engine.coins}")
                        Text(if (r.failed) "失败" else "成功", color = if (r.failed) Color.Red else Color(0xFF2E7D32))
                        Spacer(modifier = Modifier.height(8.dp))
                        Row {
                            Button(onClick = {
                                level = engine.loadLevel(engine.currentLevelId)
                                engine.generateOrders(level)
                                screen = Screen.Planning
                            }) { Text("重玩") }
                            Spacer(modifier = Modifier.width(8.dp))
                            Button(onClick = {
                                if (!r.failed) engine.currentLevelId += 1
                                level = engine.loadLevel(engine.currentLevelId)
                                engine.generateOrders(level)
                                screen = Screen.Planning
                            }, enabled = !r.failed) { Text("下一关") }
                        }
                    }
                }
                Screen.Shop -> {
                    Text("商店")
                    LazyColumn(modifier = Modifier.weight(1f)) {
                        items(shopItems) { item ->
                            Card(Modifier.fillMaxWidth().padding(4.dp)) {
                                Row(Modifier.padding(8.dp), verticalAlignment = Alignment.CenterVertically) {
                                    Text(item.name)
                                    Spacer(Modifier.weight(1f))
                                    Button(onClick = {
                                        if (engine.coins >= item.price) engine.coins -= item.price
                                    }) { Text("${item.price} 购买") }
                                }
                            }
                        }
                    }
                    Button(onClick = { screen = Screen.Main }) { Text("返回") }
                }
            }
        }
    }

    if (showEvent) {
        AlertDialog(
            onDismissRequest = { showEvent = false },
            title = { Text("事件") },
            text = { Text(eventText) },
            confirmButton = {
                Text("等待", modifier = Modifier.clickable {
                    engine.applyEvent(0.1f, 0.05f)
                    showEvent = false
                })
            },
            dismissButton = {
                Text("绕行", modifier = Modifier.clickable {
                    engine.applyEvent(0.05f, 0.02f)
                    showEvent = false
                })
            }
        )
    }
}

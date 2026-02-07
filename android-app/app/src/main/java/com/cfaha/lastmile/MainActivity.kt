package com.cfaha.lastmile

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
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

@Composable
fun App() {
    MaterialTheme {
        Column(
            modifier = Modifier.fillMaxSize().background(Color(0xFFF5F5F5)).padding(16.dp),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text("快递员：最后一公里", style = MaterialTheme.typography.headlineMedium, fontWeight = FontWeight.Bold)
            Spacer(modifier = Modifier.height(12.dp))
            Text("Kotlin 版本原型", color = Color.Gray)
            Spacer(modifier = Modifier.height(24.dp))

            var orders by remember { mutableStateOf(3) }
            var coins by remember { mutableStateOf(0) }

            Text("订单数：$orders")
            Row {
                Button(onClick = { orders++ }) { Text("+1") }
                Spacer(modifier = Modifier.width(8.dp))
                Button(onClick = { if (orders > 0) orders-- }) { Text("-1") }
            }
            Spacer(modifier = Modifier.height(16.dp))
            Button(onClick = { coins += orders * 10 }) { Text("模拟配送") }
            Spacer(modifier = Modifier.height(8.dp))
            Text("金币：$coins")
        }
    }
}

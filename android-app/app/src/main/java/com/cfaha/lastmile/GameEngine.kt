package com.cfaha.lastmile

import kotlin.math.max
import kotlin.random.Random

class GameEngine(
    private val levels: List<LevelConfig>,
    private val eventWeights: EventWeightsConfig,
    private val orderWeights: OrderWeightsConfig
) {
    var currentLevelId: Int = 1
    var coins: Int = 0
    var orders: MutableList<Order> = mutableListOf()
    var onTimeRate: Float = 1f
    var efficiency: Float = 1f
    var satisfaction: Float = 1f
    var dailyCompleted: Int = 0
    var dailyTarget: Int = 5
    var dailyRewardClaimed: Boolean = false

    fun loadLevel(levelId: Int): LevelConfig {
        currentLevelId = levelId
        return levels.firstOrNull { it.id == levelId } ?: levels.first()
    }

    fun generateOrders(level: LevelConfig) {
        orders.clear()
        repeat(level.orders) { idx ->
            val type = pickType()
            val dist = Random.nextDouble(0.5, 3.0).toFloat()
            val timeLimit = max(60, level.time - typeTimePenalty(type))
            orders.add(Order(idx + 1, type, dist, timeLimit))
        }
        onTimeRate = 1f
        efficiency = 1f
        satisfaction = 1f
    }

    fun deliverNext(speedMultiplier: Float = 1f): Pair<Order?, Boolean> {
        val next = orders.firstOrNull { !it.delivered } ?: return Pair(null, true)
        val travelSeconds = (next.distanceKm * 180f / max(0.1f, speedMultiplier)).toInt()
        val onTime = travelSeconds <= next.timeLimit
        next.delivered = true
        dailyCompleted += 1
        if (!onTime) {
            onTimeRate = (onTimeRate - 0.1f).coerceAtLeast(0f)
            satisfaction = (satisfaction - 0.05f).coerceAtLeast(0f)
        }
        return Pair(next, orders.all { it.delivered })
    }

    fun applyEvent(penalty: Float, satisfactionPenalty: Float) {
        efficiency = (efficiency - penalty).coerceAtLeast(0.6f)
        satisfaction = (satisfaction - satisfactionPenalty).coerceAtLeast(0f)
    }

    fun calcScore(): Float {
        return onTimeRate * 0.4f + efficiency * 0.3f + satisfaction * 0.3f
    }

    fun calcReward(base: Int = 500): Int {
        val score = calcScore()
        val multiplier = 0.8f + score
        return (base * multiplier).toInt()
    }

    fun isFailed(): Boolean = orders.any { !it.delivered }

    fun pickEvent(level: LevelConfig): String {
        val weights = when (level.zone) {
            ZoneType.Residential -> eventWeights.residential
            ZoneType.Commercial -> eventWeights.commercial
            ZoneType.Industrial -> eventWeights.industrial
        }
        val r = Random.nextFloat()
        return when {
            r < weights.gate -> "Gate"
            r < weights.gate + weights.rain -> "Rain"
            else -> "Traffic"
        }
    }

    fun applyForcedEvent(type: String) {
        when (type) {
            "Gate" -> applyEvent(0.1f, 0.05f)
            "Rain" -> applyEvent(0.15f, 0.02f)
            "Traffic" -> applyEvent(0.2f, 0.04f)
        }
    }

    private fun pickType(): OrderType {
        val r = Random.nextFloat()
        val n = orderWeights.normal
        val f = orderWeights.fresh
        val i = orderWeights.insured
        val l = orderWeights.large
        return when {
            r < n -> OrderType.Normal
            r < n + f -> OrderType.Fresh
            r < n + f + i -> OrderType.Insured
            r < n + f + i + l -> OrderType.Large
            else -> OrderType.Night
        }
    }

    private fun typeTimePenalty(type: OrderType): Int = when (type) {
        OrderType.Fresh -> 30
        OrderType.Night -> 20
        OrderType.Large -> 15
        else -> 0
    }
}

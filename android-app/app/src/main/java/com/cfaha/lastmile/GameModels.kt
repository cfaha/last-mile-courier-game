package com.cfaha.lastmile

import org.json.JSONObject

enum class OrderType { Normal, Fresh, Insured, Large, Night }
enum class ZoneType { Residential, Commercial, Industrial }

data class Order(
    val id: Int,
    val type: OrderType,
    val distanceKm: Float,
    val timeLimit: Int,
    var delivered: Boolean = false
)

data class LevelConfig(
    val id: Int,
    val orders: Int,
    val time: Int,
    val eventChance: Float,
    val zone: ZoneType,
    val forcedEvent: String
)

data class EventWeights(val gate: Float, val rain: Float, val traffic: Float)

data class EventWeightsConfig(
    val residential: EventWeights,
    val commercial: EventWeights,
    val industrial: EventWeights
)

data class OrderWeightsConfig(
    val normal: Float,
    val fresh: Float,
    val insured: Float,
    val large: Float,
    val night: Float
)

data class ShopItem(val id: String, val name: String, val price: Int)

data class GameResult(
    val delivered: Int,
    val total: Int,
    val score: Float,
    val onTimeRate: Float,
    val efficiency: Float,
    val coinsEarned: Int,
    val failed: Boolean
)

object JsonParser {
    fun parseLevels(json: String): List<LevelConfig> {
        val root = JSONObject(json)
        val arr = root.getJSONArray("levels")
        return List(arr.length()) { i ->
            val o = arr.getJSONObject(i)
            LevelConfig(
                id = o.getInt("id"),
                orders = o.getInt("orders"),
                time = o.getInt("time"),
                eventChance = o.getDouble("eventChance").toFloat(),
                zone = ZoneType.valueOf(o.getString("zone")),
                forcedEvent = o.getString("forcedEvent")
            )
        }
    }

    fun parseEventWeights(json: String): EventWeightsConfig {
        val root = JSONObject(json)
        fun w(key: String): EventWeights {
            val o = root.getJSONObject(key)
            return EventWeights(
                gate = o.getDouble("gate").toFloat(),
                rain = o.getDouble("rain").toFloat(),
                traffic = o.getDouble("traffic").toFloat()
            )
        }
        return EventWeightsConfig(
            residential = w("residential"),
            commercial = w("commercial"),
            industrial = w("industrial")
        )
    }

    fun parseOrderWeights(json: String): OrderWeightsConfig {
        val o = JSONObject(json)
        return OrderWeightsConfig(
            normal = o.getDouble("normal").toFloat(),
            fresh = o.getDouble("fresh").toFloat(),
            insured = o.getDouble("insured").toFloat(),
            large = o.getDouble("large").toFloat(),
            night = o.getDouble("night").toFloat()
        )
    }

    fun parseShop(json: String): List<ShopItem> {
        val root = JSONObject(json)
        val arr = root.getJSONArray("items")
        return List(arr.length()) { i ->
            val o = arr.getJSONObject(i)
            ShopItem(
                id = o.getString("Id"),
                name = o.getString("Name"),
                price = o.getInt("Price")
            )
        }
    }
}

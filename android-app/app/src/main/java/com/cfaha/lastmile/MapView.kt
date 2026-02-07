package com.cfaha.lastmile

import androidx.compose.foundation.Canvas
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Offset
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.drawscope.Stroke
import androidx.compose.ui.unit.dp
import kotlin.math.cos
import kotlin.math.sin

@Composable
fun MapView(orders: List<Order>, currentId: Int?) {
    Canvas(modifier = Modifier.fillMaxWidth().height(160.dp)) {
        val count = orders.size
        if (count == 0) return@Canvas
        val radius = size.minDimension * 0.35f
        val center = Offset(size.width / 2, size.height / 2)
        val points = orders.mapIndexed { i, _ ->
            val angle = (Math.PI * 2 * i / count).toFloat()
            Offset(
                center.x + radius * cos(angle),
                center.y + radius * sin(angle)
            )
        }
        // draw lines
        for (i in 0 until count - 1) {
            drawLine(Color.DarkGray, points[i], points[i + 1], strokeWidth = 4f)
        }
        // nodes
        orders.forEachIndexed { i, o ->
            val baseColor = if (o.delivered) Color.White else OrderTypeColorKt.get(o.type)
            val color = if (o.id == currentId) Color.Red else baseColor
            drawCircle(
                color = color,
                radius = 12f,
                center = points[i]
            )
            drawCircle(Color.Black, radius = 12f, center = points[i], style = Stroke(2f))
        }
    }
}

object OrderTypeColorKt {
    fun get(type: OrderType): Color = when (type) {
        OrderType.Fresh -> Color(0xFF4CAF50)
        OrderType.Insured -> Color(0xFF2196F3)
        OrderType.Large -> Color(0xFFFF9800)
        OrderType.Night -> Color(0xFF9C27B0)
        else -> Color(0xFFBDBDBD)
    }
}

package com.cfaha.lastmile

import androidx.compose.foundation.gestures.detectDragGestures
import androidx.compose.foundation.layout.offset
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.pointer.pointerInput
import androidx.compose.ui.unit.IntOffset
import kotlin.math.roundToInt

@Composable
fun draggableReorder(
    index: Int,
    onMoveUp: () -> Unit,
    onMoveDown: () -> Unit
): Modifier {
    var offsetY by remember { mutableStateOf(0f) }
    return Modifier
        .offset { IntOffset(0, offsetY.roundToInt()) }
        .pointerInput(index) {
            detectDragGestures(
                onDragEnd = { offsetY = 0f },
                onDragCancel = { offsetY = 0f }
            ) { change, dragAmount ->
                change.consume()
                offsetY += dragAmount.y
                if (offsetY < -40) { onMoveUp(); offsetY = 0f }
                if (offsetY > 40) { onMoveDown(); offsetY = 0f }
            }
        }
}

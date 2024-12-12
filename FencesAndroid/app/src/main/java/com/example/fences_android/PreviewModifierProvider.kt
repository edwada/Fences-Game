package com.example.fences_android

import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.PreviewParameterProvider

class PreviewModifierProvider : PreviewParameterProvider<Modifier> {
    override val values = sequenceOf(
        Modifier.fillMaxSize()
    )
}
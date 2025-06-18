package com.example.pandu_gogi_android.data.local

import android.content.Context
import android.util.Log
import android.widget.Toast

object Helper {
    fun log(string: String) {
        Log.d("ApiError", "Eror : ${string}")
    }

    fun toast(context: Context, string: String) {
        Toast.makeText(context, string, Toast.LENGTH_SHORT).show()
    }
}
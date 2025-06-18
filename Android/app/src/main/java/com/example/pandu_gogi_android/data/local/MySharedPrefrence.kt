package com.example.pandu_gogi_android.data.local

import android.content.Context

object MySharedPrefrence {
    val token = "token"
    val shared_key = ""

    fun saveToken(context: Context, tokenUser: String) {
        val shared = context.getSharedPreferences(shared_key, Context.MODE_PRIVATE)
        with(shared.edit()) {
            putString(tokenUser, token)
            apply()
        }
    }

    fun getToken(context: Context): String? {
        val shared = context.getSharedPreferences(shared_key, Context.MODE_PRIVATE)
        return shared.getString(token, null)
    }

    fun deleteToken(context: Context) {
        val shared = context.getSharedPreferences(shared_key, Context.MODE_PRIVATE)
        with(shared.edit()) {
            remove(token)
            apply()
        }
    }
}
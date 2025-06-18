package com.example.pandu_gogi_android.view.ui.activity

import android.content.Intent
import android.os.AsyncTask
import android.os.Bundle
import android.util.Log
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import com.example.pandu_gogi_android.MainActivity
import com.example.pandu_gogi_android.R
import com.example.pandu_gogi_android.data.local.Helper
import com.example.pandu_gogi_android.data.local.MySharedPrefrence
import com.example.pandu_gogi_android.data.model.HttpModel
import com.example.pandu_gogi_android.data.network.HttpHandler
import com.example.pandu_gogi_android.databinding.ActivityLoginBinding
import org.json.JSONObject

class LoginActivity : AppCompatActivity() {
    lateinit var binding: ActivityLoginBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.apply {
            tvRegister.setOnClickListener {
                startActivity(Intent(this@LoginActivity, RegisterActivity::class.java))
                overridePendingTransition(R.anim.fade_in, R.anim.fade_out)
                finish()
            }

            btn.setOnClickListener {
                if (etUsername.text.toString() == "" || etPassword.text.toString() == "") {
                    Helper.toast(this@LoginActivity, "All fields must be filled")
                    return@setOnClickListener
                }

                login(this@LoginActivity).execute()
            }
        }
    }

    class login(private val activity: LoginActivity): AsyncTask<String, Void, HttpModel>() {
        override fun doInBackground(vararg p0: String?): HttpModel {
            return try {
                val json = JSONObject().apply {
                    put("username", activity.binding.etUsername.text.toString())
                    put("password", activity.binding.etPassword.text.toString())
                }

                HttpHandler().request(
                    "user/login",
                    "POST",
                    null,
                    json.toString()
                )
            } catch (e: Exception) {
                Helper.log(e.message!!)
                HttpModel(500, "${e.message}")
            }
        }

        override fun onPostExecute(result: HttpModel?) {
            super.onPostExecute(result)
            if (result != null) {
                val body = JSONObject(result.body)
                Log.d("codeResponse", result.code.toString())

                if (result.code in 200..300) {
//                    if (body.getString("role") == "admin") return Helper.toast(activity, "Your role is admin")
                    val token = body.getString("token")
                    MySharedPrefrence.saveToken(activity, token)

                    Helper.toast(activity, "Login success")
                    activity.startActivity(Intent(activity, MainActivity::class.java))
                    activity.overridePendingTransition(R.anim.fade_in, R.anim.fade_out)
                    activity.finish()
                } else {
                    Helper.toast(activity, "Your data is not valid!")
                }
            }
        }
    }
}
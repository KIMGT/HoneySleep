package com.example.myhome.smart_pillow;

/*
    시작화면
    로그인 버튼을 누르면 정보 있으면 로그인되고 없으면 실패라고 뜸
    회원가입 버튼 누르면 회원가입 페이지로 이동
 */
import android.content.Intent;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.toolbox.Volley;

import org.json.JSONObject;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final TextView idText = (TextView)findViewById(R.id.idText);
        final TextView passwordText = (TextView)findViewById(R.id.passwordText);
        final Button loginButton = (Button)findViewById(R.id.loginButton);
        final TextView registerButton = (TextView)findViewById(R.id.registerButton);

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override

            public void onClick(View v) {
                final String userID = idText.getText().toString();
                final String userPassword = passwordText.getText().toString();

                Response.Listener<String> response = new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        try{
                            JSONObject jsonObject = new JSONObject(response);
                            boolean success = jsonObject.getBoolean("success");
                            if(success)
                            {
                                String userID = jsonObject.getString("userID");
                                String userPassword = jsonObject.getString("userPassword");

                                Intent loginIntent = new Intent(LoginActivity.this, MainActivity.class);
                                loginIntent.putExtra("userID",userID);
                                loginIntent.putExtra("userPassword",userPassword);
                                LoginActivity.this.startActivity(loginIntent);
                            }
                            else
                            {
                                AlertDialog.Builder builder = new AlertDialog.Builder(LoginActivity.this);
                                builder.setMessage("실패하였습니다")
                                        .setNegativeButton("확인",null)
                                        .create()
                                        .show();
                            }
                        }
                        catch (Exception e)
                        {
                            e.printStackTrace();
                        }
                    }
                };
                LoginRequest login = new LoginRequest(userID,userPassword,response); // LoginRequest.LoginRequest 실행
                RequestQueue requestQueue = Volley.newRequestQueue(LoginActivity.this); // 인터넷 접속함
                requestQueue.add(login); // 리퀘스트 큐에 LoginRequest 정보 추가
            }
        });
        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent registerIntent = new Intent(LoginActivity.this, RegisterActivity.class);
                LoginActivity.this.startActivity(registerIntent);
            }
        });

    }
}

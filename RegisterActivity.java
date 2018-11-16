package com.example.myhome.smart_pillow;

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
/*
    회원가입 페이지
    버튼을 클릭하면 php 파일로 정보를 보내서 성공하면 true값을 가져와서 if(success)문 실행
    실패하면 false값 가져와서 esle문 실행
 */
public class RegisterActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        final TextView idText = (TextView)findViewById(R.id.idText);
        final TextView passwordText = (TextView)findViewById(R.id.passwordText);
        final Button registerButton = (Button)findViewById(R.id.registerButton);
        final TextView nameText = (TextView)findViewById(R.id.nameText);
        final TextView ageText=(TextView)findViewById(R.id.ageText);

        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String userID = idText.getText().toString();
                String userPassword = passwordText.getText().toString();
                String userName = nameText.getText().toString();
                int userAge = Integer.parseInt(ageText.getText().toString());

                Response.Listener<String> responseListner = new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        try{
                            JSONObject jsonResponse = new JSONObject(response);
                            boolean success = jsonResponse.getBoolean("success");
                            if(success)
                            {
                                AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                                builder.setMessage("성공하였습니다")
                                        .setPositiveButton("확인",null)
                                        .create()
                                        .show();

                                Intent loginIntent = new Intent(RegisterActivity.this, LoginActivity.class);
                                RegisterActivity.this.startActivity(loginIntent);
                            }
                            else
                            {
                                AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
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

                RegisterRequest registerRequest = new RegisterRequest(userID,userPassword,userName,userAge,responseListner);
                RequestQueue queue = Volley.newRequestQueue(RegisterActivity.this);
                queue.add(registerRequest);

            }
        });
    }
}

package com.example.myhome.smart_pillow;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;

import java.util.HashMap;
import java.util.Map;

public class LoginRequest extends StringRequest {
    final static private String URL = "http://csy7792.cafe24.com/Login.php";
    private Map<String, String> parameters;

    public LoginRequest(String userID, String userPassword, Response.Listener<String> listener)
    {
        /*
            "http://csy7792.cafe24.com/Login.php" 에 POST 방식으로 정보를 보내줌
        */
        super(Method.POST,URL,listener,null);
        parameters = new HashMap<>();
        parameters.put("userID",userID); // 맵에 userID 값 넣어줌
        parameters.put("userPassword",userPassword);   // 맵에 userPassword 값 넣어줌
    }
    public Map<String,String> getParams()
    {
        return parameters;
    }
}

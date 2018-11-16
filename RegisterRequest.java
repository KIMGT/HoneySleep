package com.example.myhome.smart_pillow;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import java.util.HashMap;
import java.util.Map;

    /*
        "http://csy7792.cafe24.com/Register.php" 로 데이터를 보내서 php 파일 실행하는 부분
     */
public class RegisterRequest extends StringRequest {
    final static private String URL = "http://csy7792.cafe24.com/Register.php";
    private Map<String, String> parameters;

    public RegisterRequest(String userID, String userPassword, String userName, int userAge, Response.Listener<String> listener)
    {
        super(Method.POST,URL,listener,null);
        parameters = new HashMap<>();
        parameters.put("userID",userID);
        parameters.put("userPassword",userPassword);
        parameters.put("userName",userName);
        parameters.put("userAge",userAge+"");
    }
    public Map<String,String> getParams()
    {
        return parameters;
    }
}

package com.example.myhome.smart_pillow;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;

import java.util.HashMap;
import java.util.Map;

public class addClass extends StringRequest {
    final static private String URL = "http://csy7792.cafe24.com/addData.php";
    private Map<String, String> parameters;

    public addClass(String userID, Response.Listener<String> listener)
    {
        super(Method.POST,URL,listener,null);
        parameters = new HashMap<>();
        parameters.put("userID",userID);
    }
    public Map<String,String> getParams()
    {
        return parameters;
    }
}

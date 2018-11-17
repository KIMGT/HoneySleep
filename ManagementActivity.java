package com.example.myhome.smart_pillow;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class ManagementActivity extends AppCompatActivity {
    private ListView listView;
    private List<Data> dataList;
    private dataListAdapter adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_management);

        Intent intent = getIntent();

        listView = (ListView)findViewById(R.id.dataListTextView);
        dataList = new ArrayList<Data>();
        adapter = new dataListAdapter(getApplicationContext(),dataList);
        listView.setAdapter(adapter);

        try{
            JSONObject jsonObject = new JSONObject(intent.getStringExtra("dataList"));
            JSONArray jsonArray = jsonObject.getJSONArray("response");
            int count=0;
            String datatype,data_;
            while(count<jsonArray.length())
            {
                JSONObject object = jsonArray.getJSONObject(count);
                datatype = object.getString("datatype");
                data_ = object.getString("data_");
                Data data = new Data(datatype,data_);
                dataList.add(data);
                count++;
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }

    }
}

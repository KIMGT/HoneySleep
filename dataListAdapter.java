package com.example.myhome.smart_pillow;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.List;

public class dataListAdapter extends BaseAdapter {
    private Context context;
    private List<Data> dataList;

    public dataListAdapter(Context context,List<Data> dataList)
    {
        this.context=context;
        this.dataList=dataList;
    }

    @Override
    public int getCount() {
        return dataList.size();
    }

    @Override
    public Object getItem(int position) {
        return dataList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View v = View.inflate(context,R.layout.user,null);
        TextView dataID= (TextView)v.findViewById(R.id.dataID);
        TextView data= (TextView)v.findViewById(R.id.data);

        dataID.setText(dataList.get(position).getDatatype());
        data.setText(dataList.get(position).getData_());

        v.setTag(dataList.get(position).getDatatype());

        return v;
    }
}

package com.example.myhome.smart_pillow;

public class Data {
    String datatype;
    String data_;

    public Data(String datatype,String data_)
    {
        this.datatype = datatype;
        this.data_ = data_;
    }

    public String getDatatype() {
        return datatype;
    }

    public void setDatatype(String datatype) {
        this.datatype = datatype;
    }

    public String getData_() {
        return data_;
    }

    public void setData_(String data_) {
        this.data_ = data_;
    }
}

package com.example.rlxo5.honeysleep;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        TextView idText = (TextView)findViewById(R.id.idText);
        TextView passwordText = (TextView)findViewById(R.id.passwordText);
        TextView titleText = (TextView)findViewById(R.id.titleText);
        Button bluetoothButton = (Button)findViewById(R.id.bluetoothButton);
        Button getdataButton = (Button)findViewById(R.id.getdataButton);

        Intent intent = getIntent();
        String userID = intent.getStringExtra("userID");
        String userPassword = intent.getStringExtra("userPassword");
        String message = "환영합니다"+userID+"님";


        idText.setText(userID);
        passwordText.setText(userPassword);
        titleText.setText(message);
        
        Button bluetoothButton = (Button)findViewById(R.id.bluetoothButton);
        
    bluetoothButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(
                        getApplicationContext(), // 현재 화면의 제어권자
                        Blue.class); // 다음 넘어갈 클래스 지정
                startActivity(intent); // 다음 화면으로 넘어간다

            }
     
            }
        });




        getdataButton.setOnClickListener(new View.OnClickListener() {
            // 데이터 가져오기 버튼 클릭하면 BackgroundTask 클래스를 이용하여 "csy7792.cafe24.com/getData.php"에서 데이터 갖옴
            @Override
            public void onClick(View v) {
                //new BackgroundTask().execute();
                TextView idText = (TextView)findViewById(R.id.idText);
                String id = idText.getText().toString();
                String method = "day";
                Calendar cal = Calendar.getInstance(); //객체 생성 및 현재 일시분초...셋팅
                int yearTime = cal.get(Calendar.YEAR);
                int monthTime = cal.get(Calendar.MONTH);

                /* DB 대조 */
                ContentValues values = new ContentValues();
                values.put("userID", id);
                values.put("method", method);
                values.put("yearTime",yearTime);
                values.put("monthTime",monthTime);

                NetworkTask networkTask = new NetworkTask("http://csy7792.cafe24.com/dataGet.php", values);
                networkTask.execute();

            }
        });
    }
    public class NetworkTask extends AsyncTask<Void, Void, String> {

        String url;
        ContentValues values;

        NetworkTask(String url, ContentValues values){
            this.url = url;
            this.values = values;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            //progress bar를 보여주는 등등의 행위
        }

        @Override
        protected String doInBackground(Void... params) {
            String result;
            RequestHttpURLConnection requestHttpURLConnection = new RequestHttpURLConnection();
            result = requestHttpURLConnection.request(url, values);
            return result; // 결과가 여기에 담깁니다. 아래 onPostExecute()의 파라미터로 전달됩니다.
        }

        @Override
        protected void onPostExecute(String result) {
            Toast.makeText(getApplicationContext(), result, Toast.LENGTH_LONG).show();
            Intent intent = new Intent(MainActivity.this,ManagementActivity.class);
            intent.putExtra("dataList",result);
            MainActivity.this.startActivity(intent);

        }
    }


    /*
    class BackgroundTask extends AsyncTask<Void,Void,String>
    {
        String target;
        @Override
        protected void onPreExecute()
        {
            target = "http://csy7792.cafe24.com/dataGet.php";
        }
        @Override
        protected String doInBackground(Void... voids)
        {
            try {
                URL url = new URL(target);

                HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
                InputStream inputStream = httpURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                String temp;
                StringBuilder stringBuilder = new StringBuilder();
                while ((temp = bufferedReader.readLine()) != null) {
                    stringBuilder.append(temp + "\n");
                }
                //String result; // 요청 결과를 저장할 변수.
                //RequestHttpURLConnection requestHttpURLConnection = new RequestHttpURLConnection();
                //result = requestHttpURLConnection.request(target, null);
                //return result;
                bufferedReader.close();
                inputStream.close();
                httpURLConnection.disconnect();
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            return null;
        }
        public void onProgressUpdate(Void... values)
        {
            super.onProgressUpdate(values);
        }
        public void onPostExecute(String result){
            Intent intent = new Intent(MainActivity.this,ManagementActivity.class);
            intent.putExtra("dataList",result);
            MainActivity.this.startActivity(intent);
        }
    }
    */
}

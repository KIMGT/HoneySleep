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
        });
    
    }
   
}

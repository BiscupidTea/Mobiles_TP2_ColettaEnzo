package com.coletta.colettalogger;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;

import java.io.BufferedWriter;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.List;
import java.io.File;
import java.io.FileWriter;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;
import android.content.pm.PackageManager;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.constraintlayout.motion.widget.Debug;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

public class Logger {
    private static final String LOGTAG = "ColettaLogg";
    List<String> logList = new ArrayList<String>();
    public String fileName;
    public static Activity unityActivity;

    public static void initialize(Activity context) {
        unityActivity = context;
    }
    AlertDialog.Builder builder;
    private static Logger _instance = null;
    public String getLOGTAG(String time) {
        return LOGTAG + time;
    }

    public void SendLog(String log) {
        logList.add(log);
        Log.d("Unity Log", log);

        SaveLogsToFile();
    }

    public void SendLog(String log, int logType) {
        String typeOfLog = "Null";
        switch (logType) {
            case 0:
                Log.v("Unity Log", log);
                typeOfLog = "Log:";
                break;
            case 1:
                Log.w("Unity Log", log);
                typeOfLog = "Warning:";
                break;
            case 2:
                Log.e("Unity Log", log);
                typeOfLog = "Error:";
                break;
            case 3:
                Log.d("Unity Log", log);
                typeOfLog = "Exception:";
                break;

        }
        logList.add(typeOfLog+log);
        SaveLogsToFile();

    }

    private static final String PERMISSION = Manifest.permission.WRITE_EXTERNAL_STORAGE;

    public void CreateAlert() {
        builder = new AlertDialog.Builder(unityActivity);
        builder.setTitle("Confirmation");
        builder.setMessage("Are you sure you want to delete the logs file?");
        builder.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                Log.v(LOGTAG, "Press yes");

                DeleteLogs();
                logList.clear();
                dialog.cancel();
            }
        });
        builder.setNegativeButton("No", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.cancel();
                Log.v(LOGTAG, "Press No");

            }
        });

    }

    public void ShowAlert() {
        AlertDialog alert = builder.create();
        builder.show();
    }

    private void SaveLogsToFile() {

        Context context = unityActivity.getApplicationContext();
        try {
            DeleteLogs();
            File logFile = new File(context.getExternalFilesDir(null), "ColettaLoggerUnity" + ".txt");
            FileWriter fileWriter = new FileWriter(logFile, true);

            for (String log : logList) {
                fileWriter.append(log).append("\n");
            }
            Log.v("FileWriter", context.getExternalFilesDir(null).toString());
            fileWriter.close();
        } catch (IOException e) {
            Log.v("FileWriter", "Failed To write");
            Toast.makeText(unityActivity.getApplicationContext(), "Failed To write", Toast.LENGTH_SHORT).show();
        }

    }

    private void DeleteLogs() {

        Context context = unityActivity.getApplicationContext();

        File logFile = new File(context.getExternalFilesDir(null), "ColettaLoggerUnity.txt");
        if (logFile.exists()) {

            if (logFile.delete()) {
                Log.i("FileDeleted", "File ColettaLoggerUnity.txt deleted successfully.");

            } else {
                Log.e("FileDeleteError", "Failed to delete file " + fileName + ".txt");

            }
        }
        else
        {
            Log.e("FileDeleteError", "No file to delete");

        }

    }
    private String readFile()
    {
        Context context = unityActivity.getApplicationContext();
        File file = new File(context.getExternalFilesDir(null),"ColettaLoggerUnity.txt");
        Log.v("FileReader", context.getExternalFilesDir(null).toString());
        byte[] content = new byte[(int)file.length()];
        if (file.exists())
        {
            try
            {
                FileInputStream inputStream =  new FileInputStream(file);
                inputStream.read(content);
                return new String(content);
            }
            catch (IOException e)
            {
                Log.e("ReadFile", "Can not read file");
                return "Can not read file";
            }
        }
        else
        {
            Log.e("ReadFile", "File not found ");
            return "File doest Exist";

        }
    }
}
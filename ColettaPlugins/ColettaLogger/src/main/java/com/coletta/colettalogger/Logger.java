package com.coletta.colettalogger;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.util.Log;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Logger {
    static final String LOGTAG = "ColettaLogg";
    static ColettaLogger _instance = null;
    private static Activity unityActivity;
    AlertDialog.Builder builder;
    public String filename;
    List<String> warnings = new ArrayList<>();
    List <String> errors = new ArrayList<>();
    List <String> debug = new ArrayList<>();

    public static void reciveUnityActivity(Activity uActivity)
    {
        unityActivity = uActivity;
    }
    public interface AlertCallback
    {
        public void onPositive(String message);
        public void onNegative(String message);
    }

    public void CreateAlert(AlertCallback alertCallback)
    {
        Log.v(LOGTAG,"Android Create Alert");
        builder = new AlertDialog.Builder(unityActivity);
        builder.setMessage("Do you want to delete the log file?");
        builder.setCancelable(false);
        builder.setPositiveButton(
                "Yes",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Log.v(LOGTAG,"Clicked From Pluggin - YES");
                        alertCallback.onPositive("Clicked Yes");
                        dialogInterface.cancel();
                    }
                }
        );
        builder.setNegativeButton(
                "No",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Log.v(LOGTAG,"Clicked From Pluggin - NO");
                        alertCallback.onNegative("Clicked NO");
                        dialogInterface.cancel();
                    }
                }
        );
    }

    public void ShowAlert()
    {
        Log.v(LOGTAG,"Android Show Alert");
        AlertDialog alert = builder.create();
        alert.show();
    }
}
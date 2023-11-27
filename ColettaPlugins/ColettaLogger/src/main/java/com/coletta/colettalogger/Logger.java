package com.coletta.colettalogger;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.util.Log;
import android.widget.Toast;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Logger {
    private static final String LOGTAG = "ColettaLogg";
    private static Logger instance = null;
    private Activity unityActivity;
    private AlertDialog.Builder builder;
    private String filename;
    private List<String> warnings = new ArrayList<>();
    private List<String> errors = new ArrayList<>();
    private List<String> debug = new ArrayList<>();

    // Método para obtener la instancia singleton
    public static Logger getInstance() {
        if (instance == null) {
            instance = new Logger();
        }
        return instance;
    }

    // Método para recibir la actividad de Unity
    public void setUnityActivity(Activity uActivity) {
        unityActivity = uActivity;
    }

    // Interfaz de Alerta (Callback)
    public interface AlertCallback {
        void onPositive(String message);
        void onNegative(String message);
    }

    // Método para escribir en el archivo
    private void writeToFile(String fileName, List<String> data) {
        Context context = unityActivity.getApplicationContext();
        File file = new File(context.getExternalFilesDir(null), fileName);

        try (FileWriter fileWriter = new FileWriter(file, true)) {
            for (String entry : data) {
                fileWriter.append(entry).append("\n");
            }
        } catch (IOException e) {
            Log.e(LOGTAG, "Error writing to file: " + e.toString());
        }
    }

    // Método para eliminar el archivo de registro
    public void deleteLogFile() {
        Context context = unityActivity.getApplicationContext();
        File logFile = new File(context.getExternalFilesDir(null), filename);

        if (logFile.exists() && logFile.delete()) {
            Toast.makeText(context, "The file: " + filename + " has been deleted", Toast.LENGTH_SHORT).show();
        } else {
            Toast.makeText(context, "The file: " + filename + " does not exist", Toast.LENGTH_SHORT).show();
        }
    }

    // Método para crear una alerta
    public void createAlert(AlertCallback alertCallback) {
        builder = new AlertDialog.Builder(unityActivity);
        builder.setMessage("Do you want to delete the log file?");
        builder.setCancelable(false);

        builder.setPositiveButton("Yes", (dialogInterface, i) -> {
            alertCallback.onPositive("Clicked Yes");
            deleteLogFile();
            dialogInterface.cancel();
        });

        builder.setNegativeButton("No", (dialogInterface, i) -> {
            alertCallback.onNegative("Clicked NO");
            dialogInterface.cancel();
        });
    }

    // Método para mostrar la alerta
    public void showAlert() {
        if (builder != null) {
            builder.create().show();
        } else {
            Log.e(LOGTAG, "Alert not created. Call createAlert() first.");
        }
    }
}
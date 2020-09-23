Set your AdMob App ID

-Android
Add your AdMob App ID to the AndroidManifest.xml file in the Assets/Plugins/Android/GoogleMobileAdsPlugin directory of your Unity app, using the <meta-data> tag as shown below. You can find your App ID in the AdMob UI. For android:value insert your own AdMob App ID in quotes.

<manifest>
    <application>
        <!-- Your AdMob App ID will look similar to this
        sample ID: ca-app-pub-3940256099942544~3347511713 -->
        <meta-data
            android:name="com.google.android.gms.ads.APPLICATION_ID"
            android:value="YOUR_ADMOB_APP_ID"/>
    </application>
</manifest>
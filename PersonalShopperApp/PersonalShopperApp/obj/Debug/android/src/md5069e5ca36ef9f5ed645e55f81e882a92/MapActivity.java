package md5069e5ca36ef9f5ed645e55f81e882a92;


public class MapActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.OnMapReadyCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnMapReady:(Lcom/google/android/gms/maps/GoogleMap;)V:__export__\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_ArrivedAtStore:(Landroid/view/View;)V:__export__\n" +
			"n_onMapReady:(Lcom/google/android/gms/maps/GoogleMap;)V:GetOnMapReady_Lcom_google_android_gms_maps_GoogleMap_Handler:Android.Gms.Maps.IOnMapReadyCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("PersonalShopperApp.Activities.MapActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MapActivity.class, __md_methods);
	}


	public MapActivity ()
	{
		super ();
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("PersonalShopperApp.Activities.MapActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnMapReady (com.google.android.gms.maps.GoogleMap p0)
	{
		n_OnMapReady (p0);
	}

	private native void n_OnMapReady (com.google.android.gms.maps.GoogleMap p0);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void ArrivedAtStore (android.view.View p0)
	{
		n_ArrivedAtStore (p0);
	}

	private native void n_ArrivedAtStore (android.view.View p0);


	public void onMapReady (com.google.android.gms.maps.GoogleMap p0)
	{
		n_onMapReady (p0);
	}

	private native void n_onMapReady (com.google.android.gms.maps.GoogleMap p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

package md5069e5ca36ef9f5ed645e55f81e882a92;


public class HomeActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_PlaceOrder:(Landroid/view/View;)V:__export__\n" +
			"n_BecShopper:(Landroid/view/View;)V:__export__\n" +
			"n_SignOut:(Landroid/view/View;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("PersonalShopperApp.Activities.HomeActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HomeActivity.class, __md_methods);
	}


	public HomeActivity ()
	{
		super ();
		if (getClass () == HomeActivity.class)
			mono.android.TypeManager.Activate ("PersonalShopperApp.Activities.HomeActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void PlaceOrder (android.view.View p0)
	{
		n_PlaceOrder (p0);
	}

	private native void n_PlaceOrder (android.view.View p0);


	public void BecShopper (android.view.View p0)
	{
		n_BecShopper (p0);
	}

	private native void n_BecShopper (android.view.View p0);


	public void SignOut (android.view.View p0)
	{
		n_SignOut (p0);
	}

	private native void n_SignOut (android.view.View p0);

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

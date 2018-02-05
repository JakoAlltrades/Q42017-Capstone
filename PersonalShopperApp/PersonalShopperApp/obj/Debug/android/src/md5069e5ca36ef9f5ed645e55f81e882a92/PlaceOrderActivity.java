package md5069e5ca36ef9f5ed645e55f81e882a92;


public class PlaceOrderActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_AddItemPage:(Landroid/view/View;)V:__export__\n" +
			"n_AddItemToOrder:(Landroid/view/View;)V:__export__\n" +
			"n_EditItemToOrder:(Landroid/view/View;)V:__export__\n" +
			"n_RemoveItem:(Landroid/view/View;)V:__export__\n" +
			"n_SetStoreAddress:(Landroid/view/View;)V:__export__\n" +
			"n_FinishStoreAddress:(Landroid/view/View;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("PersonalShopperApp.Activities.PlaceOrderActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PlaceOrderActivity.class, __md_methods);
	}


	public PlaceOrderActivity ()
	{
		super ();
		if (getClass () == PlaceOrderActivity.class)
			mono.android.TypeManager.Activate ("PersonalShopperApp.Activities.PlaceOrderActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void AddItemPage (android.view.View p0)
	{
		n_AddItemPage (p0);
	}

	private native void n_AddItemPage (android.view.View p0);


	public void AddItemToOrder (android.view.View p0)
	{
		n_AddItemToOrder (p0);
	}

	private native void n_AddItemToOrder (android.view.View p0);


	public void EditItemToOrder (android.view.View p0)
	{
		n_EditItemToOrder (p0);
	}

	private native void n_EditItemToOrder (android.view.View p0);


	public void RemoveItem (android.view.View p0)
	{
		n_RemoveItem (p0);
	}

	private native void n_RemoveItem (android.view.View p0);


	public void SetStoreAddress (android.view.View p0)
	{
		n_SetStoreAddress (p0);
	}

	private native void n_SetStoreAddress (android.view.View p0);


	public void FinishStoreAddress (android.view.View p0)
	{
		n_FinishStoreAddress (p0);
	}

	private native void n_FinishStoreAddress (android.view.View p0);

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

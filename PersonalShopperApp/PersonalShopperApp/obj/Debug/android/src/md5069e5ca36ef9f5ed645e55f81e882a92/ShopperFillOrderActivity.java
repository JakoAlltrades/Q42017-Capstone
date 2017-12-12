package md5069e5ca36ef9f5ed645e55f81e882a92;


public class ShopperFillOrderActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_ItemFound:(Landroid/view/View;)V:__export__\n" +
			"n_ItemMissing:(Landroid/view/View;)V:__export__\n" +
			"n_FoundItems:(Landroid/view/View;)V:__export__\n" +
			"n_MissingItems:(Landroid/view/View;)V:__export__\n" +
			"n_BackToOrder:(Landroid/view/View;)V:__export__\n" +
			"n_FinishOrder:(Landroid/view/View;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("PersonalShopperApp.Activities.ShopperFillOrderActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ShopperFillOrderActivity.class, __md_methods);
	}


	public ShopperFillOrderActivity ()
	{
		super ();
		if (getClass () == ShopperFillOrderActivity.class)
			mono.android.TypeManager.Activate ("PersonalShopperApp.Activities.ShopperFillOrderActivity, PersonalShopperApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void ItemFound (android.view.View p0)
	{
		n_ItemFound (p0);
	}

	private native void n_ItemFound (android.view.View p0);


	public void ItemMissing (android.view.View p0)
	{
		n_ItemMissing (p0);
	}

	private native void n_ItemMissing (android.view.View p0);


	public void FoundItems (android.view.View p0)
	{
		n_FoundItems (p0);
	}

	private native void n_FoundItems (android.view.View p0);


	public void MissingItems (android.view.View p0)
	{
		n_MissingItems (p0);
	}

	private native void n_MissingItems (android.view.View p0);


	public void BackToOrder (android.view.View p0)
	{
		n_BackToOrder (p0);
	}

	private native void n_BackToOrder (android.view.View p0);


	public void FinishOrder (android.view.View p0)
	{
		n_FinishOrder (p0);
	}

	private native void n_FinishOrder (android.view.View p0);

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

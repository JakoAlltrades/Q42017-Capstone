package md5dd049f7c39a64ea42e1a1ae643ef0f84;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_SignIn:(Landroid/view/View;)V:__export__\n" +
			"n_CreateAccount:(Landroid/view/View;)V:__export__\n" +
			"n_PlaceOrder:(Landroid/view/View;)V:__export__\n" +
			"n_PrevOrders:(Landroid/view/View;)V:__export__\n" +
			"n_BecShopper:(Landroid/view/View;)V:__export__\n" +
			"n_prevDeliv:(Landroid/view/View;)V:__export__\n" +
			"n_SignOut:(Landroid/view/View;)V:__export__\n" +
			"n_CreateAccountNext:(Landroid/view/View;)V:__export__\n" +
			"n_FinishCreation:(Landroid/view/View;)V:__export__\n" +
			"n_AddItemPage:(Landroid/view/View;)V:__export__\n" +
			"n_AddItemToOrder:(Landroid/view/View;)V:__export__\n" +
			"n_EditItemToOrder:(Landroid/view/View;)V:__export__\n" +
			"n_RemoveItem:(Landroid/view/View;)V:__export__\n" +
			"n_SetStoreAddress:(Landroid/view/View;)V:__export__\n" +
			"n_FinishStoreAddress:(Landroid/view/View;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("PersonalShopperApplication.MainActivity, PersonalShopperApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("PersonalShopperApplication.MainActivity, PersonalShopperApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void SignIn (android.view.View p0)
	{
		n_SignIn (p0);
	}

	private native void n_SignIn (android.view.View p0);


	public void CreateAccount (android.view.View p0)
	{
		n_CreateAccount (p0);
	}

	private native void n_CreateAccount (android.view.View p0);


	public void PlaceOrder (android.view.View p0)
	{
		n_PlaceOrder (p0);
	}

	private native void n_PlaceOrder (android.view.View p0);


	public void PrevOrders (android.view.View p0)
	{
		n_PrevOrders (p0);
	}

	private native void n_PrevOrders (android.view.View p0);


	public void BecShopper (android.view.View p0)
	{
		n_BecShopper (p0);
	}

	private native void n_BecShopper (android.view.View p0);


	public void prevDeliv (android.view.View p0)
	{
		n_prevDeliv (p0);
	}

	private native void n_prevDeliv (android.view.View p0);


	public void SignOut (android.view.View p0)
	{
		n_SignOut (p0);
	}

	private native void n_SignOut (android.view.View p0);


	public void CreateAccountNext (android.view.View p0)
	{
		n_CreateAccountNext (p0);
	}

	private native void n_CreateAccountNext (android.view.View p0);


	public void FinishCreation (android.view.View p0)
	{
		n_FinishCreation (p0);
	}

	private native void n_FinishCreation (android.view.View p0);


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

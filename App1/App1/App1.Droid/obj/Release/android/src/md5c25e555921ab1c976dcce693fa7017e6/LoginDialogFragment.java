package md5c25e555921ab1c976dcce693fa7017e6;


public class LoginDialogFragment
	extends md5c25e555921ab1c976dcce693fa7017e6.AbstractBuilderDialogFragment_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Acr.UserDialogs.Fragments.LoginDialogFragment, Acr.UserDialogs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginDialogFragment.class, __md_methods);
	}


	public LoginDialogFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoginDialogFragment.class)
			mono.android.TypeManager.Activate ("Acr.UserDialogs.Fragments.LoginDialogFragment, Acr.UserDialogs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
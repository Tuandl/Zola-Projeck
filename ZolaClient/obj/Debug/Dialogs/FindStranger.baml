   M S B A M L   `   `   `  ����E  AZolaClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ZolaClient.DialogsZolaClient.Dialogs  5   	   %    ZolaClient.Dialogs.FindStranger   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ZolaClient.Dialogs     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Icon$" /Resources/img/zola-icon.png��5	   	    � Title$ Explore Friends��5
   	   $��562.7��6!   $	��300��60    � 	Resources 5      /   )ZolaClient.Dialogs.StringToImageConverter   StringToImageConverter  StrangerItem �� 	Converter J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius��%`  &	        &	      7     5   
   6'   �� 5   
   .R�5      ]� $��3q�6   .+�5      �� $
��Left=�6   $��70��6.   $��70��69   $��UniformToFill=�5   e   ��6   �� *	AvatarUrl+8   6e   �� 5      $��8,0,0,0q�6   $��Center=�6.   $��Center=�6I   $��0��6f   .+�5      �� $
��Bold"�6:   $��14(�6L   ��6    �� *
Username+6L   �� 5      ��6    �� *Name+�� 5      -   5        $��0,3,0,0q�5   2   $	��100��6C    5        5      $ 	#FF77876C��6/   $ 15Q�6A   5      .��5   )   Make friend67   5      5      5      5      ��5      
� 5   
   .��5      �  � Color$ 	#FFF4FFF3��6   	 � Offset$	 1Q�6-   � 5       $ 	#FFBCFFB5��6   5!      .��5#      ]� .+�5$   
   ]� $	��Top=�6   $��5q�6)   .+�5%      �� $
��Left=�6   $��0,2,6,0q�6.   	Username:6J   �� 5&      $��3q�6   
 �� CornerRadius$
 10��6)   ���	#FF597856._�5'      �� -   5        $��txtStrangerUsername��5'   H   $��0q�6c   $��0q�6n   $��201.77��6�   ��6�   Z� ��6�   Z� 5(      5)      �� 5*   
   -   5        $��lvStrangers��5*      ��6&   Z� ��6<   Z� 6U   5+      5,      
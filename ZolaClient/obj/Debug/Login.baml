   M S B A M L   `   `   `  ����E  AZolaClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ZolaClient
ZolaClient  5   	       ZolaClient.Login   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ZolaClient     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5       � Icon$" /Resources/img/zola-icon.png��5   	    � Title$
 Zola��5	   	   $��348.797��6   $��331.357��6'    � 
ResizeMode$ CanMinimize=�5
   	   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFF5F1D6��6    � Offset$ 0Q�6-   � 5      $ 	#FFFFE1A0��6   $ 1Q�6-   5       � ContextMenu 5      �� 5   
   .@�5      m� -   5        $��ctmIPConfig��5   +   $��
_IP config��6   6>   5      .��5      �� $��10q�6   $��Center=�6   .+�5   
   �� $"��/Resources/img/zola-logo.png��6   $��70��66   $��70��6B   $��40q�6M   � 5   
   .+�5      �� $��	Username:��6   $
��10 0q�6)   �� 5      -   5        $��txtUsername��5      $	��150��6)   5      � 5   
   $��Right=�6   $
��0 10q�60   .+�5      �� $��	Password:��6   $
��10 0q�6)   G� 5      -   5        $��txtPassword��5      $	��150��6-   5       � 5"   
   $��Center=�6   $
��0 20q�61   .+�5#      �� -   5        $��btnLogin��5#      $��Login��6%   $��60��5$      $	��5 0q�6    	 �� 	IsDefault	 . 
 J� Effect
 5%      3  -System.Windows.Media.Effects.DropShadowEffect  5&         
BlurRadius$ 7Q�6'      Color$ 	#FF384237��66      	Direction$	 276Q�6H      Opacity$ 0.885Q�6X      ShadowDepth$ 3Q�6h   ��5(      �� 5)      $ 0.5,1&�6*    � MappingMode$ RelativeToBoundingBox=�6;   $ 0.5,0&�6_   .��5*      � $ 	#FF9AD677��6'   $ 1Q�69   � 5+      $ 	#FFDBEC90��6'   5,      5.      �� 5/      -   5        $��btnRegister��5/      $��	Regitster��6(   $��60��50      $	��5 0q�6    
 51        52      $ 7Q�6'   $ 	#FF384237��66   $	 276Q�6H   $ 0.885Q�6X   $ 3Q�6h   ��54      �� 55      $ 0.5,1&�6*   $ RelativeToBoundingBox=�6;   $ 0.5,0&�6_   .��56      � $ 	#FF9AD677��6'   $ 1Q�69   � 57      $ 	#FFDBEC90��6'   58      5:      5;      5<      5=      
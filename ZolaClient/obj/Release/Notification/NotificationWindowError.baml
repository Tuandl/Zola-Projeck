   M S B A M L   `   `   `  ����   
ZolaClient5    /ZolaClient.Notification.NotificationWindowError   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Notification Popup��$��241.237�� � SizeToContent$ Height=� � WindowStyle$
 None=� � AllowsTransparency . 
������  � WindowStartupLocation$ Manual=� � ShowInTaskbar .  .���  J� RenderTransformOrigin$	 0,1&�.+��� $��1q����	#FFFF6B6B
���   � �� CornerRadius$ 10��$��
0,0,-9.5,0q�._��� $��20q�.+��� $
��Wrap=�$��5q��� .��� -   $��
txtblTitle��Title�� �� �� -   $��txtblMessage��$��WrapWithOverflow=�$	��200���message message message message message message message message message message message message message message message message � Triggers :� 	 :� RoutedEvent$	 FrameworkElement.Loaded��.[��� .���� -   .��U� 
 �� TargetProperty$9
 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�.\���  O� KeyTime$ 0:0:0�� O� Value$ 0Q��� $ 0:0:0.5��$ 1Q�U� $
 (UIElement.Opacity)�.\��� $ 0:0:2��$ 1Q��� $ 0:0:4��$ 0Q�z���  �� ScaleY$ 1Q�
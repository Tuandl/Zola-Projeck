   M S B A M L   `   `   `  ���� E  AZolaClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ZolaClient
ZolaClient      ZolaClient.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:ZolaClient     -     � Icon$"  /Resources/img/zola-icon.png�� � Title$
 Zola��$��673.03��$��1042.941�� � WindowStartupLocation$ CenterScreen=� � 	Resources '   !ZolaClient.StringToImageConverter   StringToImageConverter  OnlineTemplate �� 	Converter&   System.Windows.Media.TextOptions   TextHintingMode  OfflineTemplate  MyMessageTemplate �� CornerRadius  FriendMessageTemplate  AvatarUserStoryBoard �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame	 O� KeyTime
 O� Value��%h  &	        &	      7   &	 �    7   &	     7   &	     7   &	       �� .R�]� $	��0 3q�.+��� $��65��$��65��$��UniformToFill=�$
��Left=����� *	AvatarUrl+8   �� $��Right=�$
��Bold"�$ Animated=�$��16(����	#FF471A1A���� *UnreadMessage+�� $��10 5 0 0q�.+��� ���� *Name+�� $��0 5 0 0q����� *
Username+� .+��� $#��Resources/img/online-icon.png��$��0,2,0,0q��� $��Online��$��0 5 0 0q��� .R�]� $	��0 3q�$��Stretch=�.+��� $��65��$��65��$
��Left=�$��UniformToFill=����� *	AvatarUrl+8   �� $��Right=�$
��Bold"�$ Animated=�$��16(����	#FF471A1A���� *UnreadMessage+�� $��10 5 0 0q�.+��� ���� *Name+�� $��0 5 0 0q����� *
Username+� .+��� $$��Resources/img/offline-icon.png��$��0,2,0,0q��� $��Offline��$��0 5 0 0q��� .R�]� $	��3 3q�.+��� $��50��$��50��$��Right=�$	��Top=�$��UniformToFill=����� *	AvatarUrl+8   �� 
�������$ 5��$��Right=�$��0,0,5,0q�._��� $��Right=�$��10q�$	��600��$
��Wrap=�$��14(����� *MessageContent+�� .R�]� $	��3 3q�.+��� $��50��$��50��$
��Left=�$	��Top=�$��UniformToFill=����� *	AvatarUrl+8   �� 
�������$ 5��$
��Left=�$��5,0,0,0q�._��� $
��Left=�$��10q�$	��600��$
��Wrap=�$��14(����� *MessageContent+�� .��U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ imgCurUserAvatar��.\�  $	 0��$	
 -84Q�  $	 0:0:0.7��$
 0.333Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ imgCurUserAvatar��.\�  $	 0��$	
 -72Q�  $	 0:0:0.7��$
 -0.333Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�$ imgCurUserAvatar��.\�  $	 0��$
 90Q�  $	 0:0:0.7��$
 0Q� � Triggers :�  :� RoutedEvent$# UIElement.MouseLeftButtonDown�� :� 
SourceName$ btnFindStranger��:� $ FrameworkElement.Loaded��.[��� #��[ .��]� .+�]� $	��Top=�]� $
��Left=�$	��229������  �� EndPoint$	 1,1&� �� 
StartPoint$	 0,1&�.���  � Color$ 	#FFE5FFAF��� $ 	#FFFFEDE4�� � Offset$ 1Q�.+�]� $	��Top=�$��5q�.+��� -   $��imgCurUserAvatar��$&�� Resources/img/avatar-default.png��$��75��$��75��$��UniformToFill=�$
��Left=� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T� �� $��10 0 0 0q�.+��� $��5q�Hello �� -   $��txtblCurUserName���� -   $��btnUpdateInformation�����	#FFFDF5F5 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 9Q�   	Direction$	 280Q�   ShadowDepth$ 2Q�.��Update Information�� -   $��btnFindStranger��$��0 5 0 0q����	#FFFDF5F5   $ 9Q�$	 280Q�$ 2Q�.��Explore Friends]� $��0 10 0 0q�.+��� $	��Top=����	#FF5891AA$��3q�$ 12��$��28.667��._�]� .+��� $
��Left=�$"��Resources/img/seach-icon.png��$��20��$��20��$��1,0,0,0q��� -   $��	txtFilter��$��186.332��$��15.333��$��0,-1,0,0q�$��0q���Z� ��Z� ]� .+��� -   $��	lvFriends��$	��Top=�$��Stretch=���Z� ��Z�  �� 
GroupStyle (  "System.Windows.Controls.GroupStyle     HeaderTemplate �� .R��� $
��Bold"�$��14(����� *Name+]� -   $��pnMainDisplay��$
��Auto=�$
��Auto=���!�  j� Stretch$
 None=� �� Opacity$	 0.8Q� !� Visual ��  -  'System.Windows.Media.Effects.BlurEffect     Radius$ 20Q�   
KernelType$ Gaussian=�����  �� CreateOptions$ PreservePixelFormat=� �� CacheOption$ OnLoad=�  �� 	UriSource$/  )Resources/img/main-panel-background-2.jpg?�! �� DecodePixelHeight$
! 1500��" �� DecodePixelWidth$
" 1500��.+��� -	   $��pnInit��$y�	Collapsed=�$��Center=�$��Center=����� *ActualWidth+# �� ElementName$# pnMainDisplay������ *ActualHeight+$# pnMainDisplay��.+��� $��	0,306,0,0q�$ �� TextAlignment$$ Center=�$��Center=�$��40(�$��DemiBold"�$��Center=�Hello �� -
   $��txtblCurUserName2��]� -   $��pnChat��$	��Top=�$y�	Collapsed=����� *ActualWidth+$# pnMainDisplay������ *ActualHeight+$# pnMainDisplay��.+��� $	��Top=�$	��112��% �� Orientation$% 
Horizontal=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF5FFE8��$ 0Q�� $ 	#FFD9E4DE��$ 1Q�.+��� -   $��imgCurFriend��$&�� Resources/img/avatar-default.png��$��20 10q�$��92��$��92��$��UniformToFill=�   &   Color$& 	#FF878787���� $% Vertical=�$��Center=�.+��� -   $��txtblNameCurFriend��$��10q�$��24(�$
��Bold"�Tên�� -   $��txtblUsernameCurFriend��$��10,-11,10,10q�$��15(�$��Italic$�
Username]� .+�]� $��Bottom=�$��42��.+��� -   $��btnSendMessage��$��Right=�$��58�����	#FFC1CDC6���	#FFC1CDC6.���� $��Resources/img/send.png��$��5q��� ' �� Padding$' 5q�$��2q�$ 10��( J� OpacityMask
( �����
����������� $ 1,0.5&�$ 0,0.5&�.��� $ 	#FFD9E1D9��$ 0Q�� $ 	#FFC8D1C9��$ 1Q�._��� -   $��txtMessageToSend��) �� TextWrapping$) WrapWithOverflow=�* �� MaxLines$* 10��$��16(�$��42��
�������$��0q�$��24���� -   $��txtblIsWritting��$��Bottom=�$��Italic$�$��0q�$��28��+ � Cursor$
+ None��, �� Padding$, 12,6,0,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFD0D8D0��$ 1Q�� $ 	#FFDDE1D6���� -   $��lvChatMessages����Z� ��Z� ���� - �� 
TargetType"- ��.��� . �� Property. � ��/ �� Value$/ Stretch=��� . � ��
/ ���� �� . � ��/ �� 0 �� 
TargetType"0 ��.R��� � -   $��pnSentRequest��$��Center=�$��Center=�$y�	Collapsed=����� *ActualWidth+$# pnMainDisplay������ *ActualHeight+$# pnMainDisplay��.+��� $
��Bold"�$��40(�$��0 170q�$��Center=�$��Center=�$$ Center=����� $# pnSentRequest��1 �� Path$1 ActualWidth�Your request is pending...�� -   $��pnReceiveRequest��$% Vertical=�$��Center=�$��Center=�$y�	Collapsed=����� $# pnMainDisplay��$1 ActualWidth����� $# pnMainDisplay��$1 ActualHeight�.+�]� $��45q�.+��� -   $��imgRequestFriend��$	��Top=�$&�� Resources/img/avatar-default.png��$	��200��$	��200��$��UniformToFill=��� $��50q�.+��� -   $��txtblRequestName��$$ Center=�$��40(�$��Italic$�Tuấn đập chai�� -   $��txtblRequestUsername��$$ Center=�$��20(�$��Italic$�$��10q�tuan�� -   $��txtblRequestGender��$$ Center=�$��30(�$��Italic$�Male�� -   $��	btnAccept��$��30q�$	��150�����	#FFB5EAEA$��2q�   $ 14Q�$& 	#FF1D2283��$	 300Q�$ 4Q�.���� $��30(�Accept
   M S B A M L   `   `   `  ���� I  EPassPilot_v2.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PassPilot_v2._0.ServicesPassPilot_v2._0.Services  .    (PassPilot_v2._0.Views.PasswordChangeView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7conv&clr-namespace:PassPilot_v2._0.Services     X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � 	Resources  2   ,PassPilot_v2._0.Services.MultivalueConverter   changeConverter��%   &	          .����    �� 
TargetType �� Property �� Value��%�   '��       '�� $     '�� H     �� " ��.���  � ��$	 0,9q��� " ��.���  � ��$	 0,9q��� " ��.���  � ��$ 0,9,0,9q�.+���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -    ��Name$ lblInfo��$��Changing Password for: ��$��Right=�$��Center=��� -   $ lblSite��$
��Left=�$��Center=����� *SiteToChange.SiteName+�� $ 
Horizontal=�$��Center=�.+��� -   $ lblCharNumber��$��Number of Characters: ��$��Right=�$��Center=�"  Xceed.Wpf.Toolkit.ByteUpDown  -     Name$ byteUpDnNumChars���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Byte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 4X�$
��Left=�$��Center=�$��75��   Value �� *SiteCopy.PasswordLength+�� ���� *SiteCopy.CharacterConfig+���� .R��� $��2��$��Center=�$��Center=����� *Label+	 d� 	IsChecked	 �� *	IsEnabled+�� $ 
Horizontal=�$��Center=�.+��� -   
 ��Name$
 btnGenerate��$��Generate��$	��100��$��0,0,10,0q�$��Right=�$��Center=����� *GenerateCommand+���� *
SiteCopy+�� -   $
 btnAdvanced��$��Advanced��$	��100��$��10,0,0,0q�$
��Left=�$��Center=����� *AdvancedCommand+��`�  `� 	Converter# [  .1���  �� Path$ SiteCopy��� $ SiteToChange��� -    ��Name$ txtBoxPassword��$	��300��$��Center=�$��Center=����� *
Password+�� $ 
Horizontal=�$��Center=�.+��� -   $
 btnSave��$
��Save��$	��100��$��0,0,10,0q�$��Right=�$��Center=����� *SaveCommand+��`� # [  .1��� $ SiteCopy��� $ SiteToChange��� $ Password��� -	   $
 	btnCancel��$��Cancel��$	��100��$��10,0,0,0q�$
��Left=�$��Center=����� *CancelCommand+
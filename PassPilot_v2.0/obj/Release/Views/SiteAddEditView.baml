   M S B A M L   `   `   `  ���� I  EPassPilot_v2.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PassPilot_v2._0.ViewsPassPilot_v2._0.Views  +    %PassPilot_v2._0.Views.SiteAddEditView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:PassPilot_v2._0.Views       � 	Resources   �� 
TargetType �� Property �� Value��%/   '��       �� " ��.���  � ��$
 20,0q�.���  � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� �� $��2* �.+��� -    ��Name$ lblSiteName��$��Site Name: ��$��0��$��0��$��Center=��� -    ��Name$ txtBoxSiteName��$��0��$��1��$��Center=�$	��200��$
��Left=����� *AddEditSite.SiteName+" �� ValidatesOnNotifyDataErrors . �� -   $ 
lblSiteURL��$��
Site URL: ��$��1��$��0��$��Center=��� -   $ txtBoxSiteURL��$��1��$��1��$��Center=�$	��200��$
��Left=����� *AddEditSite.SiteURL+ . �� -   $ lblUsername��$��	Username ��$��2��$��0��$��Center=��� -   $ txtBoxUsername��$��2��$��1��$��Center=�$	��200��$
��Left=����� *AddEditSite.Username+�� -   $ lblPassword��$��
Password: ��$��3��$��0��$��Center=��� -   $ txtBoxPassword��$��3��$��1��$��Center=�$	��110��$
��Left=�$
��0,19q����� *AddEditSite.Password+�� -	   	 ��Name$	 btnGeneratePassword��$��Generate New��$��3��$��1��$��Center=�$��80��$��Right=�$��0,0,43,0q����� *GenerateCommand+�� -
   $	 btnSave��$
��Save��$��4��$��20��$��50��$��2��$��	0,0,100,0q����� *SaveCommand+�� -   $	 	btnCancel��$��Cancel��$��4��$��20��$��50��$��2��$��	100,0,0,0q����� *CancelCommand+
   M S B A M L   `   `   `  ����G  CGameLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameLauncherGameLauncher  5   	       GameLauncher.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:GameLauncher     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Ego Rush - Runner Game��5
   	   $	��576��6(   $
��1080��65   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��17* �6   �� 5      $	��61* �6   �� 5      $��1003* �6   .+�5   
   �� $��UniformToFill=�6   $��images/background.jpg��6(   $
��Left=�6G   $��3��6b   �� 5   
   -   5         ��Name$ VersionText��5      $��Bottom=�6)   $��Right=�6D   $��20(�6`   
�������$��0,0,5,0q�6�   $��Version: 0.0.1��6�    �� Padding$ 10q�6�   $��2��6�   �� 5   
   -   5        $ CreatorsText��5      $��Bottom=�6*   $
��Left=�6E   $��20(�6`   
�������$'��!Bru Thomas and Mendiboure Nicolas��6�   $ 10q�6�   $��3��6�   �� 5   
   -   5        $��
PlayButton��5      $��Checking For Updates��6#   $��60��6[   $��28(�6g   $
��Bold"�6u   ���	#DDFFFFFF$��
308,0,0,15q�6�   $��Bottom=�6�   $
��Left=�6�   $	��150��6�   $��	10,1,10,1q�6�   ���	#FFFFD616$��2��6"  5      5      
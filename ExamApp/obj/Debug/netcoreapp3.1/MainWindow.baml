   M S B A M L   `   `   `  ����B  >ExamApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ExamAppExamApp  5   	       ExamApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ExamApp     5   	     � Title$  ExamApp��5   	   $	��450��6   $	��800��6&   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��53* �6   �� 5      $
��213* �6   �� 5      $
��105* �6   �� 5      $	��29* �6    � RowDefinitions 5   
   �� 5      $
��113* �6   �� 5      $	��48* �6   �� 5      $
��269* �6   .+�5   
   �� -   5         ��Name$	 uid��5      $
��Left=�6   $��30��6:   $��18,0,0,0q�6F    �� TextWrapping$
 Wrap=�6X   $��Center=�6l   $	��207��6�   $��1��6�   �� 5   
   $��ID Number :��6   $
��Left=�6&   $��42��6A   $��23,0,0,0q�6M   $��Center=�6_   $	��110��6z   $
��Bold"�6�   $��15(�6�   $��2��6�   �� 5   
   -   5         ��Name$ btnLogin��5      $��Login��6#   $
��Left=�63   $��30��6N   $��	230,0,0,0q�6Z   $��Center=�6m   $	��108��6�   $��1��6�   ���	#FF76E02D6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         AutoGenerateColumns$ False��6%   $��Stretch=�6A   $	��251��6_   $��
20,110,0,0q�6l   $��3��6�   $��Stretch=�6�   $	��463��6�   $��1��6�   $��2��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	  ��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *id+  5      $	 	ID Number��6%   $	
 100 68    6D   �� *id_num+  5      $
	 Date��6%   $	
 100 63    6@   �� *date+  5      $	 Status��6%   $	
 156 65    6A   �� *status+5      5!      5"      
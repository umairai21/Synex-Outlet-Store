   M S B A M L   `   `   `  ����G  CAssignment 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Assignment_2Assignment_2  5   	       Assignment_2.displayItems   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Assignment_2     5   	     � Title$  Inventory Items��5   	   $	��450��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5      � 
�������.+�5   
   �� -   5         ��Name$
 LBL1��5      $��	INVENTORY��6   $��Center=�62   $	��Top=�6O   $��0,10,0,0q�5      $��Times New Roman)�6"   $��30(�6?   $
��Bold"�6M   
���   �6_   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
myDataGrid��5      $��20,60,20,60q�6E      AutoGenerateColumns$ False��6Z      CanUserAddRows$ False��5         CanUserDeleteRows$ False��6*      
IsReadOnly$
 True��5      	   GridLinesVisibility$		 All=�6%   
   AlternatingRowBackground

 �������5   '   �� *Items+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Stock Code��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W      FontSize$ 14Q�6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	StockCode+6a     5      $ 	Item Code��6%   $ * 6U   $ 14Q�6_    68   �� *
ItemCode+6_     5      $ 	Item Name��6%   $ * 6U   $ 14Q�6_    68   �� *
ItemName+6_     5      $ 
Item Price��6%   $ * 6W   $ 14Q�6a    69   �� *	ItemPrice+6a     5      $ Item Quantity��6%   $ * 6a   $ 14Q�6k    6<   �� *QuantityReceived+6k     5      $ Expiry Date��6%   $ * 6x   $ 14Q�6�    6:   �� *
ExpiryDate+ �� StringFormat$ {0:yyyy-MM-dd}��6�     5      $ Shelf Number��6%   $ * 6W   $ 14Q�6a    6;   �� *	ShelfNo+6a   5      �� 5"   
   -   5         ��Name$
 btn1��5"      $
��BACK��6   $��Right=�6N   $��	0,10,20,0q�6j   $	��Top=�6}   $��Times New Roman)�5#      $��15(�6.   $	��100��6<   $��40��6H   
�������
���   ���5"   .   �� *BackCommand+5$      5%      5&      
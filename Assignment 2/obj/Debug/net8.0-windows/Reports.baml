   M S B A M L   `   `   `  ����G  CAssignment 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Assignment_2Assignment_2  5   	       Assignment_2.Reports   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Assignment_2     5   	     � Title$  Reports��5   	   $	��450��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�62    � 
ResizeMode$ NoResize=�6W   .��5
      � 
�������.+�5   
   �� $��Back to Home��6   $��Right=�6H   $��Bottom=�6d   $	��139��5      $��28��6   $��Times New Roman)�6)   $��16(�6F   $��
0,0,21,379q�6T   
�������
���   ���5   (   �� *BackCommand+5      �� 5   
   $��10,35,10,10q�6   $��16(�6*   $��Times New Roman)�68   .@�5      �� $��Total Sales Report��6   .��5      � 
�������.+�5      �� $��Show Total Sales Report��6   $��Center=�5      $	��Top=�6:   $��0,20,0,0q�6R   $	��200��6d   $��40��6p   
���������5   ?   �� *ShowTotalSalesReportCommand+5      &   System.Windows.Controls.DataGrid  5      -   5        $��TotalSalesDataGrid��5         AutoGenerateColumns$ False��6b   $��10,80,10,10q�5         
IsReadOnly$
 True��64      GridLinesVisibility$	 All=�6F   ��5   9   �� *TotalSalesReport+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Bill Serial No��61      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6j   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6I   �� *BillSerialNo+6j     5      $ 	Item Code��61   $	 120 6a   	 6D   �� *
ItemCode+6a     5      $ 	Item Name��61   $	 120 6a   	 6D   �� *
ItemName+6a     5       $ Quantity��61   $	 120 6`   	 6C   �� *
Quantity+6`     5!      $ Price��61   $	 120 6Z   	 6@   �� *Price+6Z     5"      $ Total Price��61   $	 150 6e   	 6F   �� *
TotalPrice+6e   5$      5%      5&      �� 5)      $��Reshelve Report��6   .��5*      � 
�������.+�5+      �� $��Show Reshelve Report��6   $��Center=�5,      $	��Top=�6:   $��0,20,0,0q�6R   $	��200��6d   $��40��6p   
���������5+   <   �� *ShowReshelveReportCommand+5-        50      -   5        $��TotalReshelveDataGrid��50      $ False��6c   $��10,80,10,10q�51      $
 True��64   $	 All=�6F   ��50   <   �� *ReshelveReport+ 52        53      $ 	Item Code��61   $	 150 6a   	 6D   �� *
ItemCode+6a     54      $ 	Item Name��61   $	 150 6a   	 6D   �� *
ItemName+6a     55      $ Quantity Received��61   $	 150 6q   	 6L   �� *QuantityReceived+6q   57      58      59      �� 5<      $��Reorder Report��6   .��5=      � 
�������.+�5>      �� $��Show Reorder Report��6   $��Center=�5?      $	��Top=�6:   $��0,20,0,0q�6R   $	��200��6d   $��40��6p   
���������5>   ;   �� *ShowReorderReportCommand+5@        5C      -   5        $��TotalReorderDataGrid��5C      $ False��6a   $��10,80,10,10q�5D      $
 True��64   $	 All=�6F   ��5C   ;   �� *ReorderReport+ 5E        5F      $ 	Item Code��61   $	 150 6a   	 6D   �� *
ItemCode+6a     5G      $ 	Item Name��61   $	 150 6a   	 6D   �� *
ItemName+6a     5H      $ Total Quantity��61   $	 150 6n   	 6I   �� *QuantityReceived+6n   5J      5K      5L      �� 5O      $��Stock Report��6   .��5P      � 
�������.+�5Q      �� $��Show Stock Report��6   $��Center=�5R      $	��Top=�6:   $��0,20,0,0q�6R   $	��200��6d   $��40��6p   
���������5Q   9   �� *ShowStockReportCommand+5S        5V      -   5        $��TotalStockDataGrid��5V      $ False��6]   $��10,80,10,10q�5W      $
 True��64   $	 All=�6F   ��5V   9   �� *StockReport+ 5X        5Y      $ 	Item Code��61   $	 120 6a   	 6D   �� *
ItemCode+6a     5Z      $ 	Item Name��61   $	 120 6a   	 6D   �� *
ItemName+6a     5[      $ 
Item Price��61   $	 120 6c   	 6E   �� *	ItemPrice+6c     5\      $ 
Stock Code��61   $	 120 6c   	 6E   �� *	StockCode+6c     5]      $ Expiry Date��61   $	 120 6�   	 6F   �� *
ExpiryDate+
 �� StringFormat$
 {0:yyyy-MM-dd}��6�     5^      $ Quantity��61   $	 150 6h   	 6C   �� *QuantityReceived+6h     5_      $ Shelf No��61   $	 150 6_   	 6C   �� *	ShelfNo+6_   5a      5b      5c      �� 5f      $��Bill Report��6   .��5g      � 
�������.+�5h      �� $��Show Bill Report��6   $��Center=�5i      $	��Top=�6:   $��0,20,0,0q�6R   $	��200��6d   $��40��6p   
���������5h   8   �� *ShowBillReportCommand+5j        5m      -   5        $��TotalBillDataGrid��5m      $ False��6[   $��10,80,10,10q�5n      $
 True��6(   $	 All=�6:   ��5m   8   �� *
BillReport+ 5o        5p      $ 	Serial No��61   $	 100 6a   	 6D   �� *
SerialNo+6a     5q      $
 Date��61   $	 100 6w   	 6?   �� *Date+$
 {0:yyyy-MM-dd}��6w     5r      $ Total Price��61   $	 100 6e   	 6F   �� *
TotalPrice+6e     5s      $ Discount��61   $	 150 6`   	 6C   �� *
Discount+6`     5t      $ Cash Received��61   $	 150 6i   	 6H   �� *CashReceived+6i     5u      $ Balance��61   $	 150 6^   	 6B   �� *	Balance+6^   5w      5x      5y      5{      5|      5}      
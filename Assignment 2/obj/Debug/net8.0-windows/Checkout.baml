   M S B A M L   `   `   `  ����G  CAssignment 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Assignment_2Assignment_2  5   	       Assignment_2.Checkout   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Assignment_2     5   	     � Title$  Checkout��5   	   $	��450��6   $	��800��6'    � WindowStartupLocation$ CenterScreen=�63    � 
ResizeMode$ NoResize=�6X   .��5
      � 
�������.+�5   
   �� $��Select Item Code:��6   $
��Left=�6,   $	��Top=�6G   $��
100,50,0,0q�5      $��Times New Roman)�6$   $��20(�6A   �� 5   
   $��Enter Quantity:��6   $
��Left=�6*   $	��Top=�6E   $��100,100,0,0q�5      $��Times New Roman)�6%   $��20(�6B   �� 5   
   $#��Do you want a bill? (Yes/No):��6   $
��Left=�68   $	��Top=�6S   $��100,150,0,0q�5      $��Times New Roman)�6%   $��20(�6B   �� 5   
   $��Enter Discount (%):��6   $
��Left=�6.   $	��Top=�6I   $��100,200,0,0q�5      $��Times New Roman)�6%   $��20(�6B   �� 5   
   $��Enter Cash Received:��6   $
��Left=�6/   $	��Top=�6J   $��100,303,0,0q�5      $��Times New Roman)�6%   $��20(�6B   �� 5   
   -   5         ��Name$ lblTotalPrice��5      $��Total Price:��6'   $
��Left=�6>   $	��Top=�6Y   $��100,320,0,0q�5      $��Times New Roman)�6%   $��25(�6B   
���  ��$y�Hidden=�6a   �� 5   
   -   5         ��Name$ cmbItemCode��5      $
��Left=�6(   $��
400,50,0,0q�6C   $	��Top=�6W   $	��170��5      $��Times New Roman)�6   $��18(�6<   �� 5    
   -   5         ��Name$ txtQuantity��5       $
��Left=�6'   $��400,100,0,0q�6B   $	��Top=�6W   $	��170��5!      $��Times New Roman)�6   $��18(�6;   �� 5"   
   -   5        $ txtBill��5"      $
��Left=�6#   $��400,150,0,0q�6>   $	��Top=�6S   $	��170��5#      $��Times New Roman)�6   $��18(�6;   �� 5$   
   -   5        $ txtDiscount��5$      $
��Left=�6'   $��400,200,0,0q�6B   $	��Top=�6W   $	��170��5%      $��Times New Roman)�6   $��18(�6;   �� 5&   
   -   5        $ txtCashReceived��5&      $
��Left=�6+   $��400,303,0,0q�6F   $	��Top=�6[   $	��170��5'      $��Times New Roman)�6   $��18(�6;   �� 5*   
   -   5        $��Calculate Total��5*      $
��Left=�6+   $��400,249,0,0q�6F   $	��Top=�6[   $	��170��5+      $��Times New Roman)�6   $��18(�6:   
����ح�6_   �� 5.   
   -   5        $��SUBMIT��5.      $��Center=�6"   $��	0,370,0,0q�6?   $	��Top=�6R   $	��120��5/      $��Times New Roman)�6   $��18(�6:   
������6`   �� 52   
   -	   5        $
��BACK��52      $��Right=�6    $��	0,20,20,0q�6<   $	��Top=�6O   $	��110��53      $��Times New Roman)�6   $��15(�6:   
�������6_   54      55      
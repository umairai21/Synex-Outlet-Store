   M S B A M L   `   `   `  ����G  CAssignment 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Assignment_2Assignment_2  5   	   "    Assignment_2.manageInventory   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Assignment_2     5   	     � Title$  Manage Inventory��5   	   $	��450��6"   $	��800��6/    � WindowStartupLocation$ CenterScreen=�6;    � 
ResizeMode$ NoResize=�6`   .��5
      � 
�������.+�5   
   �� -   5         ��Name$
 lbl1��5      $��Enter Stock Code:��6   $
��Left=�6:   $	��Top=�6U   $��	50,70,0,0q�5      $��Times New Roman)�6#   $��20(�6@   �� 5   
   -   5        $
 lbl2��5      $��Select Item Code:��6   $
��Left=�6:   $	��Top=�6U   $��
50,130,0,0q�5      $��Times New Roman)�6$   $��20(�6A   �� 5   
   -   5        $
 lbl3��5      $��Enter Received Quantity:��6   $
��Left=�6A   $	��Top=�6\   $��
50,190,0,0q�5      $��Times New Roman)�6$   $��20(�6A   �� 5   
   -   5        $
 lbl4��5      $��Enter Expiry Date:��6   $
��Left=�6;   $	��Top=�6V   $��
50,250,0,0q�5      $��Times New Roman)�6$   $��20(�6A   �� 5   
   -   5        $
 lbl5��5      $ ��Select Shelf (S1, S2, S3):��6   $
��Left=�6C   $	��Top=�6^   $��
50,310,0,0q�5      $��Times New Roman)�6$   $��20(�6A   �� 5   
   -   5         ��Name$ txtbox1��5      $
��Left=�6>   $	��Top=�6Y   $��
350,70,0,0q�5      $	��300��6&   $��Times New Roman)�62   $��18(�6O    �� TextWrapping$
 Wrap=�5      ��5   #   �� *	StockCode+5      �� 5   
   -   5         ��Name$ comboItemCode��5      $
��Left=�6v   $	��Top=�6�   $��350,130,0,0q�5      $	��300��6'   $��Times New Roman)�63   $��18(�6P   ��5   *   �� *	ItemCodes+ �� SelectedItem 6L   �� *SelectedItemCode+5   P   �� 5!   
   -   5        $ txtbox3��5!      $
��Left=�6E   $	��Top=�6`   $��350,190,0,0q�5"      $	��300��6'   $��Times New Roman)�63   $��18(�6P   $
 Wrap=�5#      ��5!   #   �� *ReceivedQuantity+5#      �� 5$   
   -	   5        $ txtbox4��5$      $
��Left=�6?   $	��Top=�6Z   $��350,250,0,0q�5%      $	��300��6'   $��Times New Roman)�63   $��18(�6P   $
 Wrap=�5&      ��5$   #   �� *
ExpiryDate+5&      �� 5'   
   -
   5        $ txtbox5��5'      $
��Left=�6<   $	��Top=�6W   $��350,310,0,0q�5(      $	��300��6'   $��Times New Roman)�63   $��18(�6P   $
 Wrap=�5)      ��5'   #   �� *	ShelfNo+5)      �� 5,   
   -   5         ��Name$ 	btnSubmit��5,      $��SUBMIT��6$   $��Center=�6W   $	��Top=�6t   $��	0,370,0,0q�5-      $	��120��6$   $��45��60   $��Times New Roman)�5.      $��20(�6.   
�������
���   �
�������$��1q�5/   (   ��5,   5   �� *SubmitCommand+5/   (   �� 52   
   -   5        $ btnBack��52      $
��BACK��6"   $��Right=�6Q   $	��Top=�6m   $��	0,30,30,0q�53      $	��100��6$   $��40��60   $��Times New Roman)�54      $��18(�6.   
�������
���   �
�������$��1q�55   (   ��52   1   �� *BackCommand+55   (   56      57      
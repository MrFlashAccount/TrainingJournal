   M S B A M L   `   `   `  ����J  FTrainingJournal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TrainingJournal.ViewsTrainingJournal.Views  5      !    TrainingJournal.Views.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      /local#clr-namespace:TrainingJournal.Views  5      .��5
      � -   5          �Name$  
LayoutRoot��5
      
�������.+�5   
   �� -   5         ��Name$ UsernameTextBlock��5      $
��Left=�5      $��295,234,0,0q�5      $	��Top=�5      $	��100��5      $��30��6    $��
Логин��5      $
��Wrap=�5      $��18.667(�5       �� TextAlignment$ Right=�5      �� 5   
   -   5        $ PasswordTextBlock��5      $
��Left=�5      $��295,269,0,0q�5      $	��Top=�5      $	��100��5      $��30��6    $��18.667(�5      $��Пароль��5      $
��Wrap=�5      $ Right=�5      �� 5   
   -   5         ��Name$ LoginTextBox��5      $��400,229,400,0q�5       $	��Top=�5!       �� TextWrapping$
 Wrap=�5"      $��30��5#      $��18.667(�5$      $��2��5%      G� 5'   
   -   5         G�Name$ PasswordBox��5'      $
��Left=�5(      $��400,264,0,0q�5)      $	��Top=�5*      $	��200��5+      $��30��6"   $��18.667(�5,      $��2��5-      �� 5/   
   -   5         ��Name$ LoginButton��5/      $��425,0,425,237q�50      $��Bottom=�51      $��
Войти��52      $	��150��53      $��30��6   $��16(�54       �� 	IsDefault . 55   +   �� 56   
   -   5        $��25��57      $��Right=�58      $��	0,0,20,20q�59      $��Bottom=�5:      $	��100��5;      $��< Назад��5<      5=      �� 5?   
   -   5        $ 
ErrorLabel��5?      $�� ��5@      $
��Left=�5A      $��400,362,0,0q�5B      $	��Top=�5C      $	��200��5D      )  #MahApps.Metro.Controls.ProgressRing  5F   
   -	   5          Name$ ProgressRing��5F       $��30��5G       $��30��6+   $��580,327,385,243q�5H       $y�Hidden=�5I       5J      5K      
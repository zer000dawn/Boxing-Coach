   M S B A M L   `   `   `  ���� G  CBoxing_Coach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Boxing_CoachBoxing_Coach      Boxing_Coach.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Boxing_Coach     -     �Name$  MainWin�� � Title$ Boxing Coach��$	��466��$	��823�� � Icon$ 	/Logo.ico��.���  � ColumnDefinitions �� $
��404* ��� $
��419* � � RowDefinitions �� $��* ��� .+��� $
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�$��20(�$��Arial)���  � Language$ es-mx�$�Routine Timer���� $
��Left=�$��	10,10,0,0q�$��1��$
��Wrap=�$��Combinations��$	��Top=�$	��138��$��Arial)�$��20(��� $
��Left=�$��
121,10,0,0q�$
��Wrap=�$��
Siguiente:��$	��Top=�$��1���� -    ��Name$ Sig_Ejercicio��$
��Left=�$��
175,10,0,0q�$
��Wrap=�$	��Top=�$	��208�� �� TextAlignment$ Center=�$��1���� -   $ 	Ejercicio��$
��Left=�$��
121,46,0,0q�$
��Wrap=�$	��Top=�$��22(�$	��228��$ Center=�$��1���� -   $ 	Countdown��$
��Left=�$��
185,85,0,0q�$
��Wrap=�$	��Top=�$��36(�$	��183��$��1���� -    ��Name$ Comenzar��$��A darle��$��Center=�$��	0,158,0,0q�$	��Top=�$��50��$��1���� -   $ Pausar��$��Pausar��$
��Left=�$��244,158,0,0q�$	��Top=�$��50��$��1���� -   $ 	Reiniciar��$��	Reiniciar��$
��Left=�$��303,158,0,0q�$	��Top=�$��65��$��1���� $
��Left=�$	��192��$��	74,10,0,0q�$��1��
�������$	��Top=�$	��294�����	#FFEAEAEA$��1���� -   	 ��Name$		 Jab��$��1 - Jab��$
��Left=�$��	93,20,0,0q�$��1��$	��Top=�$��1���� -	   $	 Cross��$��	2 - Cross��$
��Left=�$��	93,40,0,0q�$��1��$	��Top=�$��1���� -
   $	 R_Hook��$��3 - Right Hook��$
��Left=�$��	93,60,0,0q�$��1��$	��Top=�$��1���� -   $	 L_Hook��$��4 - Left Hook��$
��Left=�$��	93,80,0,0q�$��1��$	��Top=�$��1���� -   $	 R_Upper��$��5 - Uppercut��$
��Left=�$��	93,98,0,0q�$��1��$	��Top=�$��1���� -   $	 S_Upper��$��6 - Small Uppercut��$
��Left=�$��
93,118,0,0q�$��1��$	��Top=�$��1���� -   $	 Liver_Hook1��$��7 - Left Liver Hook��$
��Left=�$��
93,138,0,0q�$��1��$	��Top=�$��1���� -   $	 Liver_Hook2��$��8 - Right Liver Hook��$
��Left=�$��
95,158,0,0q�$��1��$	��Top=�$��1���� -   $
	 Over��$��9 - Overhand��$
��Left=�$��
95,178,0,0q�$��1��$	��Top=�$��1���� -   $	 Min��$
��Left=�$��	10,52,0,0q�$��1��$
��Wrap=�$	��Min��$	��Top=��� -   $	 Max��$
��Left=�$��
109,52,0,0q�$��1��$
��Wrap=�$	��Max��$	��Top=��� -   $ Combi_Countdown��$
��Left=�$��
291,37,0,0q�$��1��$
��Wrap=�$	��Top=�$��36(�$	��104���� -   $ Combi_Comenzar��$��Comenzar��$
��Left=�$��
63,126,0,0q�$��1��$	��Top=��� -   $ 
Combi_Pusa��$��Pausar��$
��Left=�$��	291,0,0,0q�$��1��$��Center=�&   System.Windows.Controls.DataGrid  -   
  Name$
 	Exercises��$��10,38,87,71q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 No.��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Numero+  $ Nombre��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 200  �� *Nombre+  $	 Seg�� �� *
Segundos+�� -   $ 	Ex_Nombre��$
��Left=�$��
350,38,0,0q�$
��Wrap=�$��Nombre:��$	��Top=��� -    ��Name$ Nombre��$
��Left=�$��
350,59,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��151��$��2���� -   $ Ex_Time��$
��Left=�$��
350,82,0,0q�$
��Wrap=�$��Duración(Segundos):��$	��Top=�$��2���� -   $ Segundos��$
��Left=�$
 Wrap=�$��Center=�$	��151��$��	350,0,0,0q�$��2���� -   $ Add_Btn��$	��Add��$
��Left=�$��350,134,0,0q�$	��Top=��� -   $ Rm_Btn��$��Remove��$
��Left=�$��
20,168,0,0q�$	��Top=��� -   $ 	Clear_Btn��$��Clear��$
��Left=�$��270,168,0,0q�$	��Top=��� -    ��Name$ 	Min_Value��$
��Left=�$��	43,50,0,0q�$��1��$	��Top=�$��40���� -   $ 	Max_Value��$
��Left=�$��
144,49,0,0q�$��1��$	��Top=�$��40���� -    $ tiempo��$
��Left=�$��	10,85,0,0q�$��1��$
��Wrap=�$��Tiempo(segundos):��$	��Top=��� -!   $ segundosCombi��$
��Left=�$��
120,84,0,0q�$��1��$
 Wrap=�$	��Top=�$��64���� -"   $ Reanudar��$��Reanudar��$
��Left=�$��	343,0,0,0q�$��1��$��Center=�
   M S B A M L   `   `   `  ����C  ?SICOAV_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SICOAV_A.MarcasSICOAV_A.Marcas  5      %    SICOAV_A.Marcas.CustomMarkerRed   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0marcasclr-namespace:SICOAV_A.Marcas     5      $��20��5      $��20��6     J� Opacity$  10Q�6    J� RenderTransformOrigin$ 0.5,0.5&�6&   .��5      � -   5         �Name$ 	grid_main��5      $��0,0,0,0q�6   .+�5
   
   A� $
��Left=�6   ���	#FF0A83FB$��20��6@   $��0,0,0,0q�6K   $��20��6\   �� 5   
   $
��Left=�6   $��20��6+   $	��Top=�67   $��20��6O   $A��;/SICOAV_A;component/imagenes/icons8-airplane-mode-on-96.png��6Z   $ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle 6&   ��  �� Path$ Angulo�T� 5      5      5      � 5   
   $��22,-1,-170,-2q�6   ���	#E50B8BC7$y�	Collapsed=�6=    � RowDefinitions 5      �� 5      �� 5      �� 5      $��0* �6     � ColumnDefinitions 5      �� 5      $	��39* �6#   �� 5      $	��60* �6#   �� 5      $	��10* �6#   �� 5       $	��42* �6#   .+�5"      �� -   5         ��Name$ txt_nombre_vuelo��5"      $��2��62   $
��Wrap=�6B   $��	AE345TG34��6V   ���	#FFFFB900$��SimSun-ExtB)�6~    �� TextAlignment$ Right=�6�   $��0,0,1,0q�6�   $��2��6�   �� 5#      -   5        $ txt_rumbo_vuelo��5#      $
��Left=�61   $
��Wrap=�6L   $	��200��6`   $��39��6k   ���	#FFFFB900$��SimSun-ExtB)�6�   $ Center=�6�   �� 5$      -   5        $ txt_velocidad_vuelo��5$      $
��Wrap=�65   $��200 Km/h��6I   ���	#FFFFB900$��SimSun-ExtB)�6p   $ Center=�6�   $��1��6�   $��1��6�   $��1,0,0,0q�6�   $��2��6�   �� 5%      -   5        $ txt_altidud_vuelo��5%      $
��Wrap=�63   $��8000 m��6G   ���	#FFFFB900$��SimSun-ExtB)�6l   $��1��6�   $ Right=�6�   �� 5&      -   5        	 ��Name$	 RectangleSeleccion��5&      $��4��64   $
��Left=�6H   $��26��6c   $��2��6o   ���	#FFF0842A$	��Top=�6�   $	��151��6�   $��2��6�   $y�	Collapsed=�6�   �� 5'      -   5        $ 
txt_tiempo��5'      $
��Left=�6,   $
��Wrap=�6G   $	��200��6[   $��60��6f   ���	#FFFFB900$��SimSun-ExtB)�6�   $ Center=�6�   $��1��6�   � 5(      -   5        $ grid_historico��5(      $��3��6+   $��1��6;   $y�Hidden=�6H   .+�5)      �� -	   5        $	 rectangle_histórico��5)      $
��Left=�69   $��13��6T   $��2��6`   $	��Top=�6t   $��42��6�   ���	#FFF511116�   �� 5*      $
��Left=�6   $��8��67   $
��Wrap=�6B   $��	SIN DATOS��6V   $	��Top=�6g   $��42��6   
�������$��OCR A Extended)�6�   $��7(�6�   $��0,3,0,0q�6�   $ Center=�6�   5,      5.      � 5/   
   -
   5        $ MyGrid��5/      .+�51      )   #SICOAV_A.Marcas.IB_DATOS_VUELO_CTRL  -   5        
  Name$
 
Rectangulo��51   *   )   #SICOAV_A.Marcas.IB_DATOS_VUELO_CTRL   PreviewMouseDown$! Rectangulo_PreviewMouseDown��6T      PreviewMouseLeftButtonUp$) #Rectangulo_PreviewMouseLeftButtonUp��6�      PreviewMouseMove$! Rectangulo_PreviewMouseMove��6�      
MouseLeave$ Rectangulo_MouseLeave��6�      RaiseCustomEvent$! Rectangulo_RaiseCustomEvent��6  $��0,0,-156,-7q�6?   6E  52      �� 54   
   .+�55      �� -   5        $��m_Linea��55      $��10,10,-156,0q�6"    � UseLayoutRounding .  ���	#DF065AEE6h   56      58      5;      
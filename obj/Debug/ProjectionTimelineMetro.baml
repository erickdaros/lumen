   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      H DNumericUpDown, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null>/clr-namespace:ControlLib;assembly=NumericUpDown
ControlLib 5      U QVlc.DotNet.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=84529da31f4eb963G4clr-namespace:Vlc.DotNet.Wpf;assembly=Vlc.DotNet.WpfVlc.DotNet.Wpf 5      ^ ZMPMS - Projection Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:VlcControlModdedVlcControlModded 6X   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 6�   A   ;MPMS___Projection_Management_System.ProjectionTimelineMetro   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^	 ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    	 5      R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   
  5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro   
  5      9System&clr-namespace:System;assembly=mscorlib   
  5      F
ControlLib/clr-namespace:ControlLib;assembly=NumericUpDown   
  5      DWpf4clr-namespace:Vlc.DotNet.Wpf;assembly=Vlc.DotNet.Wpf   
  5      ;VlcControlModdedclr-namespace:VlcControlModded   
  6X   RForms@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms   
  6�   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  TimelineWindow��5   �    � Title$ Projection Timeline��5      $
��1256��5	      $	��667��5
         VSWindowStyleKey#���   5       � WindowStartupLocation$ CenterScreen=�5         NonActiveBorderBrush �	#FF164361    NonActiveWindowTitleBrush �	#FF2D2D30���	#FF2D2D30 � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Colors.xaml?�6%   �� 5      $J Dpack://application:,,,/MahApps.Metro;component/Styles/VS/Styles.xaml?�6%   %    5       � DataContext 5        5   
   .��5       � .+�5"   
   o� $��2 0 0 0q�6   $	��Top=�5#      	 J� SnapsToDevicePixels	 . .@�5%      m� $��ROCKETS��6   .@�5&      m� $��Rocket slot 1��6   .@�5'      m� $��	Fire slot��6   �� 5(      m� 5)      $��Relaod slot��6   m� 5*      $#��Report damage to control room��6   5+      m� 5,      $��Rocket slot 2��6   .@�5-      m� $��damaged .. being repaired��6   {�.  6B   5.      5/      m� 50      $��
NAVIGATION��6   .@�51      m� $��Left engine��6   .@�52      m� $��Full speed ahead��6   m� 53      $��	2/3 speed��6   m� 54      $��	1/3 speed��6   m� 55      $��Stop engine��6   56      m� 57      $��Right engine��6   .@�58      m� $��Full speed ahead��6   m� 59      $��	2/3 speed��6   m� 5:      $��	1/3 speed��6   m� 5;      $��Stop engine��6   5<      5=      5>      �� 5?   
   -   5        
 ��Name$
 sldTimeline��5?   6     
FlatSlider#���  6   $
��Left=�6K   $��
10,307,0,0q�6f   $	��Top=�6z   $	��777��6�    � Maximum$
 1000Q�6�    �� IsMoveToPointEnabled . $��14��6}  ���	#FF007ACC6�  o� 5@   
   -   5         o�Name$
 menu��5@      $
��Left=�6   $��24��68   $	��Top=�6D   $
��1248��6\   .@�5A      m� 5B      �� 5C   
   $��Lista de Importação��6   $
��Left=�60   $	��273��6K   $��	10,29,0,0q�6X   $	��Top=�6k   $	��650��6�   ���	#FF007ACC J� OpacityMask �	#FF007ACC���	#FF007ACC.��5D      � $
��Left=�6   $	��255��6.   $	��Top=�6;   $	��648��6S   $��	0,0,-2,-4q�6_   .+�5E      �� -   5         ��Name$ 
btnAddFile��5E      $��	Adicionar��6-   $
��Left=�6A   $��
10,225,0,0q�6\   $	��Top=�6p   $��75��6�   6�   5F      5G      �� 5H   
   -   5         ��Name$ gpbControls��5H      $��	Controles��6(   $
��Left=�6;   $	��273��6V   $��
665,29,0,0q�6c   $	��Top=�6w   $	��122��6�   ���	#FF007ACC���	#FF007ACC.��5I      � -   5         �Name$ grpGrid��5I      $
��Left=�6$   $	��254��6?   $��	0,0,-2,-3q�6L   $	��Top=�6_   $	��112��6w   .+�5J      �� -   5        $ btnSync��5J      $
��Sync��6*   $
��Left=�69   $��	20,87,0,0q�6T   $	��Top=�6g   $��75��6   6�   �� 5K      -   5        $ btnPlay��5K      $
��Play��6*   $
��Left=�69   $��	20,12,0,0q�6T   $	��Top=�6g   $��75��6    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5L      V� 5M      .~�5N      ��  �� ScaleX$ 1Q�6-   �� 5O      �� 5P      T� 5Q      5R      5T      �� 5U      -	   5        $ btnPause��5U      $��Pause��6+   $
��Left=�6;   $��	20,37,0,0q�6V   $	��Top=�6i   $��75��6�   6�   �� 5V      -
   5        $ btnStop��5V      $
��Stop��6*   $
��Left=�69   $��	20,62,0,0q�6T   $	��Top=�6g   $��75��6   6�   �� 5W      -   5         ��Name$ cbxIsVideoVisible��5W      $��Exibir Vídeo��66   $
��Left=�6M   $��
12,137,0,0q�6h   $	��Top=�6|   $��90��6�    d� 	IsChecked$
 TrueY�6�   �� 5X      -   5        $ cbxIsProjectionVisible��5X      $��Projetar Vídeo��6;   $
��Left=�6T   $��
12,157,0,0q�6o   $	��Top=�6�   $��99��6�   $
 TrueY�6�   �� 5Y      -   5        $
 	sldVolume��5Y   >   #���  6   $
��Left=�6Q   $��	8,196,0,0q�6l   $	��Top=�6   $	��102��6�   $ 0.5,0.5&�6�   $	 125Q�6�    � Value$	 100Q�6�   ���	#FF007ACCz�5Z      V� 5[      .~�5\      �� �� 5]      �� 5^       �� Angle$ 0Q�6.   T� 5_      5`      5b      �� 5c      -   5         ��Name$ 	lblVolume��5c      $��Volume: 100%��6+   $
��Left=�6B   $��
15,211,0,0q�6]   $	��Top=�6q   $��87��6�   �� 5d      -   5        $ btnMute��5d      $
��Mute��6*   $
��Left=�69   $��
20,112,0,0q�6T   $	��Top=�6h   $��75��6�   6�   5e      5f      �� 5g   
   $��--:--:--��6   $
��Left=�6#   $��1182,302,0,0q�6>   $	��Top=�6T   ���	#FF007AE0$��Segoe UI Semibold)�6�   $
��Bold"�6�   $��16(�6�   �� 5h   
   $��Opçoes de Vídeo��6   $
��Left=�6,   $	��296��6G   $��792,330,0,0q�6T   $	��Top=�6i   $	��446��6�   ���	#FF007ACC.��5i      � $
��Left=�6   $	��276��6.   $	��Top=�6;   $	��428��6S   $��	0,0,-2,-2q�6_   .+�5j      *   $MahApps.Metro.Controls.NumericUpDown  -   5          Name$ nudBrightness��5j   )   $��Right=�6@   $��
0,75,252,0q�6\   $	��Top=�6p   $��86��6�   ���	#FF007ACC$��3��6�    5k        5l         Maximum$	 400Q�61      Minimum$ 0Q�6?      Value$	 200X�6K   5n      �� 5o      -   5        $ cbxInvertHorizontally��5o      $��Inverter Horizontalmente��6:   $
��Left=�6]   $��266,104,0,0q�6x   $	��Top=�6�   $	��162��6�   �� 5p      -   5        $ cbxInvertVertically��5p      $��Inverter Verticalmente��68   $
��Left=�6Y   $��266,126,0,0q�6t   $	��Top=�6�   $	��162��6�   �� 5q      -   5        $ cbxInvertOnlyProjection��5q      $ ��Inverter apenas Projeção��6<   $
��Left=�6_   $��
266,80,0,0q�6z   $	��Top=�6�   $	��162��6�   $��21��6�   �� 5r      -   5         ��Name$ cbxAspectRatio��5r      $
��Left=�63   $��
343,53,0,0q�6N   $	��Top=�6b   $��85��6z    �� SelectedIndex$ 0��6�   .@�5s      �� $��Original��6#   �� 5t      $
��16:9��6#   �� 5u      $	��4:3��6#   5v      �� 5w      $��Aspect Ratio:��6   $
��Left=�60   $��
264,52,0,0q�6K   $	��Top=�6_   $��86��6w   �� 5x      $��Brilho��6   $
��Left=�6)   $��	14,75,0,0q�6D   $	��Top=�6W   �� 5y      $��	Contraste��6   $
��Left=�6,   $��
14,104,0,0q�6G   $	��Top=�6[   �� 5z      $��Saturação��6   $
��Left=�6,   $��
14,133,0,0q�6G   $	��Top=�6[   �� 5{      $
��Gama��6   $
��Left=�6'   $��
14,162,0,0q�6B   $	��Top=�6V   �� 5|      $	��HUE��6   $
��Left=�6&   $��
14,192,0,0q�6A   $	��Top=�6U     5}      -   5        $ nudContrast��5}   )   $��Right=�6>   $��0,104,252,0q�6Z   $	��Top=�6o   $��86��6�   ���	#FF007ACC$��3��6�    5~        5      $	 400Q�61   $ 0Q�6?   $	 200X�6K   5�        5�      -   5        $ nudSaturation��5�   )   $��Right=�6@   $��0,133,252,0q�6\   $	��Top=�6q   $��86��6�   ���	#FF007ACC$��3��6�    5�        5�      $	 400Q�61   $ 0Q�6?   $	 200X�6K   5�        5�      -   5        $ nudGamma��5�   )   $��Right=�6;   $��0,162,252,0q�6W   $	��Top=�6l   $��86��6�   ���	#FF007ACC$��3��6�    5�        5�      $	 400Q�61   $ 0Q�6?   $	 200X�6K   5�        5�      -   5        $ nudHUE��5�   )   $��Right=�69   $��0,192,252,0q�6U   $	��Top=�6j   $��86��6�   ���	#FF007ACC$��3��6�    5�        5�      $	 400Q�61   $ 0Q�6?   $	 200X�6K   5�      5�      5�      �� 5�   
   
���   �$
��Left=�6!   $	��264��6<   $��
792,38,0,0q�6I   
���   �$	��Top=�6l   $	��446��6�     Vlc.DotNet.Wpf.VlcControl  5�   
   -   5          Name$ timeline��5�      $
��Left=�6+   $	��264��6F   $��
792,38,0,0q�6S   $	��Top=�6g   $	��446��6   $ 0.5,0.5&�6�   z�5�      V� 5�      .~�5�      �� $ 1Q�6%   �� 5�      �� 5�      T� 5�      5�      5�        5�   
   -   5        $ vctAux��5�      $
��Left=�6)   $��41��6D   $��
586,58,0,0q�6P   $	��Top=�6d   $��45��6|   $ 0.5,0.5&�6�   $y�Hidden=�6�   z�5�      V� 5�      .~�5�      �� $ 1Q�6%   �� 5�      �� 5�      T� 5�      5�      5�      �� 5�   
   -   5          ��Name$  lsbTestList��5�      $
��Left=�6'   $	��123��6B   $	��Top=�6O   $	��777��6g   $��
10,335,0,0q�6s   �� 5�   
   -   5        ! ��Name$! lsvTestList��5�      $
��Left=�6(   $	��163��6C   $��
10,463,0,0q�6P   $	��Top=�6d   $	��777��6|   " �� View" 5�      �� 5�      .P�5�      �� 5�      5�      5�      5�      
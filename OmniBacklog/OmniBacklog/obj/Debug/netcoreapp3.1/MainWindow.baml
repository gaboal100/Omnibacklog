   M S B A M L   `   `   `  ����  OmniBacklog    OmniBacklog.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	      MaterialDesignBody#���   5   	   $��Regular"�5      $��13(�5	      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5
         TextRenderingMode$
 Auto=�5        MaterialDesignFont#���  5       � Title$ Login��5   	   $	��300��6   $	��450��6$    � 
ResizeMode$ NoResize=�60   .��5      � -   5         �Name$ gridUsuarios��5       � ColumnDefinitions 5   
   �� 5      $
��0.7* �6   �� 5      �� 5      $
��0.7* �6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      �� 5      $
��0.5* �6   �� 5       � 	Resources 5   
   ��%    5      .+�5   
   �� $��1��6   $��Center=�6    $��Center=�6=   $��16(�6X   .��6f   Iniciar Sesión6v   �� 5!   
   $��Usuario:��6   $��Right=�6#   $��0,0,10,0q�6?   $��1��6Q   $��Center=�6^   �� 5"   
   $��Contraseña:��6   $��Right=�6&   $��0,0,10,0q�6B   $��2��6T   $��Center=�6a   �� 5#   
   -   5         ��Name$ 	TBUsuario��5#      $��1��6%   $
��Left=�65   $��22��6P   $��10,0,0,0q�6\   $��1��6n   	 �� TextWrapping$
	 Wrap=�6{   $��Center=�6�   $	��250��6�   
���   �$��2��6�   G� 5$   
   -   5        
 G�Name$
 PBContraseña��5$      $��2��6,   $��1��69   $
��Left=�6I   $��22��6d   $��10,0,0,0q�6p   $��Center=�6�   $	��250��6�   $y�Visible=�6�   
���   �$��2��6�   �� 5&   
   -   5         ��Name$ BTLogin��5&      $��Login��6"   $��Center=�62   $��0q�6O   $��4��6Z   $��Center=�6g   $��90��6�   $��14(�6�   $��2��6�   �� 5'   
   -   5        $ BTRegistrar��5'      $��	Registrar��6&   $��Center=�6:   $��0q�6W   $��4��6b   $��Center=�6o   $��90��6�   $��14(�6�   $��0��6�   6�   5*      5+      
   M S B A M L   `   `   `  ����K  GCalcEventDensity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CalcEventDensity.HelpersCalcEventDensity.Helpers  5      I)clr-namespace:CalcEventDensity.ViewModelsCalcEventDensity.ViewModels  5      %    CalcEventDensity.Views.MainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=17.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9helper&clr-namespace:CalcEventDensity.Helpers     5      @
viewModels)clr-namespace:CalcEventDensity.ViewModels     5        N� Title$  MainPage��5
       � DataContext 5      /   )CalcEventDensity.ViewModels.MainViewModel  5   
    � 	Resources 5      .   (CalcEventDensity.Helpers.DoubleConverter   DoubleConverter��%   &	          5   
   6!   5      .��5      � $��Stretch=�5       � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��Center=�6"   .+�5      �� $��0, 20, 0, 5q�6   $	��150��6+   $��30��67   $��ВЫБРАТЬ ФАЙЛ��5        
BaseButton#��[ 5      ��5      �� *ChooseFileCommand+�� 5      $��NoWrap=�6   $
��10 5q�6.     BaseText#��[ 5   5    �� TextAlignment$ Center=�5      ��5      �� *ChoosedFile+5      5       �� 5"   
   $��1��6   $'��!Параметры расчета��6    $��5q�5#      $��10q�6   $	��Top=�6+     Smal#��[ 5$      .��5%      �� $��0q�6   $��Stretch=�6/   .@�5&      	�  	� 	GroupName$ 
Dimensions��6   $��2D��65   $��5q�6B   	� 5'      $ 
Dimensions��6   $��5q�65   $��3D��6@    d� 	IsChecked 5(      �� *IsSelected3D+�� 5)      $,��&Добавить точки сетки��6   $��5q�6:    5*      �� *IsGridPoints+�� 5+       �� Orientation$ 
Horizontal=�6   $��5q�66   .+�5,      �� -   5         ��Name$ 
slGridStep��5,      	 �� Delay$	 0��61   $��Stretch=�6<   $	��130��6Z   
 � Minimum$
 1Q�5-       � Maximum$ 50Q�6)    �� IsSelectionRangeEnabled .  � ToolTip$$ Задать шаг точек��5.   <    � Value 5-   6   �� *pointRadius+5.   <   �� 5/      -   5         ��Name$ 
tbGridStep��5/      $��30��62   $	��5 0q�6=   ��50      ��  �� ElementName$ 
slGridStep�� �� Path$ Value� �� Mode$ TwoWay=� �� 	Converter# [   �� UpdateSourceTrigger$ PropertyChanged=�52      53      54      �� 55   
   $��2��6   #��[ 6   $��Center=�56      $��Center=�6,   $��РАСЧЕТ��57      $	��120��6"   $��50��6.     Big#��[ 6:   ��58      �� *CalculateCommand+��59      �� $ 
tbGridStep��$
 Text�5:      5;      
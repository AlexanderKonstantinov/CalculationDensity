   M S B A M L   `   `   `  ���� K  GCalcEventDensity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CalcEventDensity.HelpersCalcEventDensity.Helpers  3clr-namespace:CalcEventDensityCalcEventDensity  !    CalcEventDensity.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=17.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9helper&clr-namespace:CalcEventDensity.Helpers     0localclr-namespace:CalcEventDensity       � Title$'  !Плотность событий��$	��400��$	��300�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � DataContext $   CalcEventDensity.MainViewModel   � 	Resources .   (CalcEventDensity.Helpers.DoubleConverter   DoubleConverter��%   &	          .��� -    �Name$ MainGrid��$��Stretch=� � RowDefinitions �� $��1* ��� $��2* ��� $��1* �.+��� $��0��$��Center=�.+��� $��0, 20, 0, 5q�$	��150��$��30��$��ВЫБРАТЬ ФАЙЛ��  
BaseButton#��[ ���� *ChooseFileCommand+�� -    ��Name$ tbChoosedFile��$��NoWrap=�$
��10 5q�  BaseText#��[  �� TextAlignment$ Center=����� *
Filename+�� $��1��$'��!Параметры расчета��$��5q�$��10q�$	��Top=�  Smal#��[ .���� $��0q�$��Stretch=�.@�	� 	 	� 	GroupName$	 
Dimensions��$��2D��$��5q�	� $	 
Dimensions��$��5q�$��3D��
 d� 	IsChecked
 �� *IsSelected3D+�� -    ��Name$ cbGridPoints��$,��&Добавить точки сетки��$

 TrueY�$��5q���  �� Orientation$ 
Horizontal=�$��5q�.+��� -    ��Name$ 
slGridStep�� �� Delay$ 0��$��Stretch=�$	��130�� � Minimum$ 1Q� � Maximum$ 50Q� �� IsSelectionRangeEnabled .  � ToolTip$$ Задать шаг точек�� � Value �� *
GridStep+�� -    ��Name$ 
tbGridStep��$��30��$	��5 0q�����  �� ElementName$ 
slGridStep�� �� Path$ Value� �� Mode$ TwoWay=� �� 	Converter# [   �� UpdateSourceTrigger$ PropertyChanged=��� $��2��#��[ $��Center=�$��Center=�$��РАСЧЕТ��$	��120��$��50��  Big#��[ ���� *CalculateCommand+
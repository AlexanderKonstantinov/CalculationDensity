   M S B A M L   `   `   `  ���� K  GCalcEventDensity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CalcEventDensity.HelpersCalcEventDensity.Helpers  I)clr-namespace:CalcEventDensity.ViewModelsCalcEventDensity.ViewModels  %    CalcEventDensity.Views.MainPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9helper&clr-namespace:CalcEventDensity.Helpers     @
viewModels)clr-namespace:CalcEventDensity.ViewModels       N� Title$  MainPage�� � DataContext /   )CalcEventDensity.ViewModels.MainViewModel   � 	Resources .   (CalcEventDensity.Helpers.DoubleConverter   DoubleConverter��%   &	          .��� $��Stretch=� � RowDefinitions �� $��1* ��� $��2* ��� $��1* �.+��� $��0��$��Center=�.+��� $��0, 20, 0, 5q�$	��150��$��30��$��ВЫБРАТЬ ФАЙЛ��  
BaseButton#��[ ���� *ChooseFileCommand+�� $��NoWrap=�$
��10 5q�  BaseText#��[  �� TextAlignment$ Center=����� *ChoosedFile+�� $��1��$'��!Параметры расчета��$��5q�$��10q�$	��Top=�  Smal#��[ .���� $��0q�$��Stretch=�.@�	�  	� 	GroupName$ 
Dimensions��$��2D��$��5q�	� $ 
Dimensions��$��5q�$��3D�� d� 	IsChecked �� *IsSelected3D+�� $,��&Добавить точки сетки��$��5q� �� *IsGridPoints+��  �� Orientation$ 
Horizontal=�$��5q�.+��� -    ��Name$ 
slGridStep��	 �� Delay$	 0��$��Stretch=�$	��130��
 � Minimum$
 1Q� � Maximum$ 50Q� �� IsSelectionRangeEnabled .  � ToolTip$$ Задать шаг точек�� � Value �� *pointRadius+�� -    ��Name$ 
tbGridStep��$��30��$	��5 0q�����  �� ElementName$ 
slGridStep�� �� Path$ Value� �� Mode$ TwoWay=� �� 	Converter# [   �� UpdateSourceTrigger$ PropertyChanged=��� $��2��#��[ $��Center=�$��Center=�$��РАСЧЕТ��$	��120��$��50��  Big#��[ ���� *CalculateCommand+���� $ 
tbGridStep��$
 Text�
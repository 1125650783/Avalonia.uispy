﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using AvaloniaUIHelp.Core;
using AvaloniaUIHelp.Messages;
using AvaloniaUIHelp.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaUIHelp.ViewModels
{
    public class SnoopViewModel : ViewModelBase,IDisposable
    {
        public SnoopViewModel(IVisual visualRoot)
        {
            TreeModel treeModel = new TreeModel();
            GetChildrens(visualRoot, treeModel);
            TreeDataSource.Add(treeModel);
            SubMessages();
        }

        public SnoopViewModel(object obj, Type type)
        {
            this.AllVisualProperties = Create(obj, type);
        }

        public SnoopViewModel()
        {
            SubMessages();
        }

        #region 消息
        public void SubMessages()
        {
            MessageAggregator<TypeMessage>.Subscribe(OnTypeMessage);
            MessageAggregator<VisualMessage>.Subscribe(OnVisualMessage);
        }

        public void UnSubMessages()
        {
            MessageAggregator<TypeMessage>.UnSubscribe(OnTypeMessage);
            MessageAggregator<VisualMessage>.UnSubscribe(OnVisualMessage);
        }

        /// <summary>
        /// 接收到类型和对象消息
        /// </summary>
        /// <param name="typeMessage"></param>
        public void OnTypeMessage(TypeMessage typeMessage)
        {

            //List<PropertyInfo> propertyInfos =
            //    typeMessage.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
            //                              BindingFlags.Static).ToList();
            //ObservableCollection<VisualProperty> lisVisualProperties = new ObservableCollection<VisualProperty>();
            ////排序
            //propertyInfos.Sort(Compare);
            //foreach (PropertyInfo propertyInfo in propertyInfos)
            //{
            //    try
            //    {
            //        VisualProperty visualProperty = new VisualProperty();
            //        visualProperty.Name = propertyInfo.Name;
            //        visualProperty.Value = propertyInfo.GetValue(typeMessage.Obj, null);
            //        visualProperty.Target = typeMessage.Obj;
            //        visualProperty.PropertyInfo = propertyInfo;
            //        if (propertyInfo.PropertyType.IsEnum)
            //        {
            //            visualProperty.TargetType = TargetType.Enum;
            //            visualProperty.LisEnumValues = Enum.GetNames(propertyInfo.PropertyType).ToList();
            //        }
            //        else if (propertyInfo.PropertyType == typeof(bool))
            //        {
            //            //布尔类型使用CheckBox
            //            visualProperty.TargetType = TargetType.Bool;
            //            visualProperty.BolValue = Convert.ToBoolean(visualProperty.Value);
            //        }
            //        lisVisualProperties.Add(visualProperty);

            //    }
            //    catch (Exception exception)
            //    {

            //    }
            //}
            this.AllVisualProperties = Create(typeMessage.Obj,typeMessage.Type);
        }

        public static ObservableCollection<VisualProperty> Create(object obj,Type type)
        {
            List<PropertyInfo> propertyInfos =
                type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                   BindingFlags.Static).ToList();
            ObservableCollection<VisualProperty> lisVisualProperties = new ObservableCollection<VisualProperty>();
            //排序
            propertyInfos.Sort(Compare);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                try
                {
                    VisualProperty visualProperty = new VisualProperty();
                    visualProperty.Name = propertyInfo.Name;
                    visualProperty.Value = propertyInfo.GetValue(obj, null);
                    visualProperty.Target = obj;
                    visualProperty.PropertyInfo = propertyInfo;
                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        visualProperty.TargetType = TargetType.Enum;
                        visualProperty.LisEnumValues = Enum.GetNames(propertyInfo.PropertyType).ToList();
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        //布尔类型使用CheckBox
                        visualProperty.TargetType = TargetType.Bool;
                        visualProperty.BolValue = Convert.ToBoolean(visualProperty.Value);
                    }
                    lisVisualProperties.Add(visualProperty);

                }
                catch (Exception exception)
                {

                }
            }
           return lisVisualProperties;
        }

        public void OnVisualMessage(VisualMessage visualMessage)
        {
            TreeModel treeModel = new TreeModel();
            GetChildrens(visualMessage.Visual, treeModel);
            TreeDataSource.Clear();
            TreeDataSource.Add(treeModel);
            SubMessages();
        }
        #endregion



        private string m_Fitter = "";

        public string Fitter
        {
            get => m_Fitter;
            set
            {
                if (m_Fitter == value)
                {
                    return;
                }

                m_Fitter = value;
                Choice(m_Fitter, AllVisualProperties, CurVisualProperties);
                this.RaisePropertyChanged(nameof(Fitter));
            }
        }

        private ObservableCollection<VisualProperty> m_AllVisualProperties = new ObservableCollection<VisualProperty>();

        public ObservableCollection<VisualProperty> AllVisualProperties
        {
            get => m_AllVisualProperties;
            set
            {
                if (m_AllVisualProperties == value)
                {
                    return;
                }

                m_AllVisualProperties = value;
                //this.RaisePropertyChanged(nameof(AllVisualProperties));
                Choice(m_Fitter, AllVisualProperties, CurVisualProperties);
            }
        }

        [Reactive] public ObservableCollection<VisualProperty> CurVisualProperties { get; set; } = new ObservableCollection<VisualProperty>();

        [Reactive] public ObservableCollection<TreeModel> TreeDataSource { get; set; } = new ObservableCollection<TreeModel>();

        private void Choice(string fitter, ObservableCollection<VisualProperty> allVisualProperties, ObservableCollection<VisualProperty> curVisualProperties)
        {
            curVisualProperties.Clear();
            string lowerFitter = fitter.ToLower();
            if (string.IsNullOrEmpty(fitter))
            {
                foreach (VisualProperty visualProperty in allVisualProperties)
                {
                    curVisualProperties.Add(visualProperty);
                }
                return;
            }
            foreach (VisualProperty visualProperty in allVisualProperties)
            {
                if (visualProperty.Name.ToLower().Contains(lowerFitter))
                {
                    curVisualProperties.Add(visualProperty);
                }
            }
        }

        public static void GetChildrens(IVisual parent, TreeModel treeModel, bool isCheckParent = true)
        {
            if (isCheckParent)
            {
                Type parentType = parent.GetType();
                treeModel.Name = parentType.Name;
                treeModel.Object = parent;
                if (parent is Control)
                {
                    //treeModel.ToolTip = DeepCopy(parent, parentType);
                }
            }
            foreach (IVisual visual in parent.VisualChildren)
            {
                Type visualType = visual.GetType();
                TreeModel childrenTreeModel = new TreeModel();
                childrenTreeModel.Name = visualType.Name;
                childrenTreeModel.Object = visual;
                treeModel.Children.Add(childrenTreeModel);

                if (visual.VisualChildren.Count > 0)
                {
                    GetChildrens(visual, childrenTreeModel, false);
                }
                if (visual is Control control)
                {
                    childrenTreeModel.Name = $"{control.Name}({visualType.Name})";
                    //if (visual is Grid grid)
                    //{
                    //    foreach (RowDefinition rowDefinition in grid.RowDefinitions)
                    //    {
                    //        TreeModel rowDefinitionModel = new TreeModel();
                    //        rowDefinitionModel.Name = nameof(RowDefinition);
                    //        childrenTreeModel.Children.Add(rowDefinitionModel);
                    //        rowDefinitionModel.Object = rowDefinition;
                    //    }

                    //    foreach (ColumnDefinition columnDefinition in grid.ColumnDefinitions)
                    //    {
                    //        TreeModel definitionModel = new TreeModel();
                    //        definitionModel.Name = nameof(ColumnDefinition);
                    //        childrenTreeModel.Children.Add(definitionModel);
                    //        definitionModel.Object = columnDefinition;
                    //    }
                    //}

                    //childrenTreeModel.ToolTip = visual;
                }

            }
        }

        public static int Compare([AllowNull] PropertyInfo x, [AllowNull] PropertyInfo y)
        {
            if (x == null)
            {
                return 1;
            }

            if (y == null)
            {
                return -1;
            }

            return string.Compare(x.Name, y.Name, true);
        }

        public void Dispose()
        {
            this.UnSubMessages();
        }
    }
}

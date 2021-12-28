# Abp.TagHelperPlus

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.TagHelperPlus%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.TagHelperPlus.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.TagHelperPlus)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.TagHelperPlus.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.TagHelperPlus)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.TagHelperPlus?style=social)](https://www.github.com/EasyAbp/Abp.TagHelperPlus)

An Abp MVC UI tag-helper enhancement module to enhance ABP built-in tag-helpers and provide new tag-helpers such as rich text editor, advanced selector, and more.

## Online Demo

We have launched an online demo for this module: [https://taghelper.samples.easyabp.io](https://taghelper.samples.easyabp.io)

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.TagHelperPlus

1. Add `DependsOn(typeof(AbpTagHelperPlusModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

## Features

### EasySelector

Improve the abp-select to support paged items and search.

   * Use abp-dynamic-form: [demo](https://github.com/EasyAbp/Abp.TagHelperPlus/blob/master/host/EasyAbp.Abp.TagHelperPlus.Web.Unified/Pages/Books/Book/ViewModels/CreateEditBookViewModel.cs#L13-L19).
      * Use in modal: [demo](https://github.com/EasyAbp/Abp.TagHelperPlus/blob/master/host/EasyAbp.Abp.TagHelperPlus.Web.Unified/Pages/Books/Book/CreateModal.cshtml#L11).
      * Use on page: [demo](https://github.com/EasyAbp/Abp.TagHelperPlus/blob/master/host/EasyAbp.Abp.TagHelperPlus.Web.Unified/Pages/Books/Book2/Index.cshtml#L33).
   * Use abp-select: [demo](https://github.com/EasyAbp/Abp.TagHelperPlus/blob/master/host/EasyAbp.Abp.TagHelperPlus.Web.Unified/Pages/Books/Book3/Index.cshtml#L33).

![EditBook](/docs/images/EasySelector/EditBook.png)

## Road map

- [x] Easy Selector
- [x] Support abp-select
- [ ] Items sorter
- [ ] Rich text editor

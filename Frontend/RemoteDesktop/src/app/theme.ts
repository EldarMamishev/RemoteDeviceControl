const primaryDark =  '#D2D453';
const successDark =  '#195b2f';
const infoDark =  '#0A88FF';
const warningDark =  '#FFCB0F';
const dangerDark =  '#FF776B';

export const DARK_THEME = {
  name: 'mydark',
  base: 'dark',
  variables: {
    primaryDark,
    successDark,
    infoDark,
    warningDark,
    dangerDark,
    charts: {
      primaryDark,
      successDark,
      infoDark,
      warningDark,
      dangerDark,
      bg: 'transparent',
      textColor: '#FEFEFF',
      axisLineColor: '#666668',
      splitLineColor: '#4A4A59',
      itemHoverShadowColor: 'rgba(0, 0, 0, 0.5)',
      tooltipBackgroundColor: '#0C0C23',
      areaOpacity: '0.7',
    },
    bubbleMap: {
      primaryDark,
      successDark,
      infoDark,
      warningDark,
      dangerDark,
      titleColor: '#FEFEFF',
      areaColor: '#4A4A59',
      areaHoverColor: '#666668',
      areaBorderColor: '#20203C',
    },
  },
};

const primaryLight =  '#0E1D6A';
const successLight =  '#247711';
const infoLight =  '#0D53A3';
const warningLight =  '#9E490C';
const dangerLight =  '#A01B28';

export const LIGHT_THEME = {
  name: 'light',
  base: 'default',
  variables: {
    primaryLight,
    successLight,
    infoLight,
    warningLight,
    dangerLight,
    charts: {
      primaryLight,
      successLight,
      infoLight,
      warningLight,
      dangerLight,
      bg: 'transparent',
      textColor: '#31447A',
      axisLineColor: '#9BB0D3',
      splitLineColor: '#B7C9E4',
      itemHoverShadowColor: 'rgba(0, 0, 0, 0.5)',
      tooltipBackgroundColor: '#CBDCF1',
      areaOpacity: '0.7',
    },
    bubbleMap: {
      primaryLight,
      successLight,
      infoLight,
      warningLight,
      dangerLight,
      titleColor: '#31447A',
      areaColor: '#B7C9E4',
      areaHoverColor: '#9BB0D3',
      areaBorderColor: '#CBDCF1',
    },
  },
};

load('dataOut.mat');
NowCastWFPFunc(dataOut(:,2));
NowCastWFPFunc(dataOut(:,2),0.5,'a',10,0.1);
NowCastWFPFunc(dataOut(:,2),0.75,'a',10,0.1);
NowCastWFPFunc(dataOut(:,2),0.75,'p',10,0.1);

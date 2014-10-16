function v_nac=wakeCalculation(Ct,i,wind)
%% v_nac = WAKECALCULATION(Ct,i,wind)
%This function calculates the wake
%Currently it is a very very simplified wake calculation. It just serves as
%a placeholder for a correct wake calculation that will come later

scaling = linspace(0.5,0.9,length(Ct));

v_nac=scaling*wind.wind(i,2);
#include <iostream>
#include <fstream>
#include <math.h>
#include <string>
using namespace std;
using std::string;

int main () {
	cout << '\t' << '\t'<< '\t' << '\t'<<"OFFWIND" <<'\n';
	cout <<endl;
	cout << '\t' << '\t' << '\t'<< "Wind-Wave interactions" << '\n';
	cout <<endl;
	cout <<endl;
const double k=0.4; 	//Von karman konstant
const double pi=3.14159265359; //pi
	double Ufriction; 	//Friction velocity
	double uf; 			//Temporarily friction velocity
	double Zo;			//Aerodynamic roughness
	double Vhub;		//Turbine hub height velocity
	double P;			//Turbine power output
	double Area;		//Turbine swept area

	
	double Ug;			//Wind speed measured at the reference height 
	cout << "Enter Wind Speed (m/s)" << endl;
	cin >> Ug ;
	
	double Zg;			//Reference height
	cout << "Enter Reference Height (m)" << '\n';
	cin >> Zg;
	
	double Zhub;		//Turbine hub height
	cout << "Enter Turbine Hub Height(m)" << '\n';
	cin >> Zhub;
	
	double Td;			//Turbine diameter
	cout << "Enter Turbine Diameter(m)" << '\n';
	cin >> Td;
	
	double Ef;			//turbine efficiency
	cout << "Enter Turbine Efficiency (%) " << '\n';
	cin >> Ef;
	
	double Cw;			//wave speed
	cout << "Enter Wave Speed" << '\n';
	cin >> Cw;

	double residual=1;Zo=2E-04;
	while (residual> 1E-07)
	{
		uf = Ug*k/(log(Zg/Zo));
		Zo=0.012*pow(uf,2)/9.81;
		Ufriction=Ug*k/(log(Zg/Zo));
		residual=uf-Ufriction;
	}
	Vhub=Ufriction*(log(Zhub/Zo))/k;
	Area=pi*pow(Td,2)/4;
	P=0.5*1.225*1.91*pow(Vhub,3)*Ef*Area/1E+08;
	cout<<endl <<endl;
	
	ofstream output;
	output.open ("output.txt",ios::trunc);
	output << '\t' << '\t'<< '\t' << '\t'<<"OFFWIND" <<'\n';
	output <<endl;
	output << '\t' << '\t' << '\t'<< "Wind-Wave interactions" << '\n';
	output <<endl;
	output <<endl;
	output << "*********************************************************" <<endl;
	output << "*********************************************************" <<endl <<endl <<endl;
	output << '\t'<< "Wind Speed" << '\t' << '\t'<< '\t'<< '\t'<< Ug << "m/s"<<'\n';
	output << '\t'<<"Reference Height" <<'\t'<< '\t' << '\t'<< Zg <<"m"<< '\n';
	output << '\t'<<"Turbine Hub Height " << '\t' << '\t'<< '\t'<< Zhub <<"m"<< '\n';
	output << '\t'<<"Turbine Diameter" << '\t' << '\t'<< '\t'<< Td <<"m"<< '\n';
	output << '\t'<<"Turbine Efficiency" << '\t' << '\t'<< '\t'<< Ef<<"%"<< '\n';
	output << '\t'<<"Wave Speed " << '\t' << '\t'<< '\t'<< '\t'<< Cw <<"m/s"<<'\n';
	output << '\t'<<"Von Karman Konstant" <<'\t'<< '\t'<< '\t'<< k <<'\n';
	output << "*********************************************************" <<endl;
	output << "*********************************************************" <<endl <<endl <<endl;
	double  Ps[5],Vhubs[5],UfrictionS[5];
	double  Zos[5]={2E-04, 2E-04, 2E-04, 2E-04, 2E-04};
	double  A []= {0.02, 0.02, 0.48, 1.89, 1.7};
	double  B[]={0.5, 0.7, -1.0, -1.59, -1.7};
	char    *arC[5];
	arC[0]=  (char*)"Toba     "; 
	arC[1]=  (char*)"Sugimori ";
	arC[2]=  (char*)"Smith    ";
	arC[3]=  (char*)"Johnson  ";
	arC[4]=  (char*)"Drennan  ";
	output<< "Method" << '\t'<<'\t'<<'\t'<<"Friction Velocity " << '\t'<< '\t'<<"Roughness Height (m)"<<'\n';
	output<<"----------------------------------------------------------------------"<<'\n';
	output<< "Charnok" <<'\t'<<'\t'<<'\t'<<'\t'<< Ufriction <<'\t'<<'\t'<<'\t'<<Zo<<'\n';
	output<<endl;
	for (int n=0;n<5;n++)	
	{
    double Error=1;
	while (Error> 1E-07)
	{
		uf = Ug*k/(log(Zg/Zos[n]));
		Zos[n]=(A[n]*pow(uf,2)/9.81)*pow(Cw/uf,B[n]);
		UfrictionS[n]=Ug*k/(log(Zg/Zos[n]));
		Error=fabs(double (uf-UfrictionS[n]));
	}  
	Vhubs[n]=UfrictionS[n]*(log(Zhub/Zos[n]))/k;
	Ps[n]=(0.5*1.225*1.91*pow(Vhubs[n],3)*Ef*Area/1E+08);
	output<<arC[n]<<'\t'<<'\t'<<'\t'<<UfrictionS[n]<<'\t'<<'\t'<<'\t'<<Zos[n]<<'\n';
	output<<endl;
	}

	output <<"-------------------------------------------------------------------------"<<'\n';
	output<<"-------------------------------------------------------------------------"<<'\n';
	output<<"-------------------------------------------------------------------------"<<'\n';
	output<< "Method" <<'\t'<<'\t'<<"Velocity"<<" m/s " << '\t'<<"P_Output"<<" MW"<<'\t'<<'\t'<<"P_Differences"<<"%"<<'\n';
	output<<endl;
	output<<"Charnok" <<'\t'<<'\t'<<Vhub<<'\t'<<'\t'<<P <<'\t'<<'\t'<<'\t'<<0 << '\t' <<'\n';
	output<<endl;
	for (int m=0; m<5;m++)
	{
		output<<arC[m]<<'\t'<<Vhubs[m]<<'\t'<<'\t'<<Ps[m]<<'\t'<<'\t'<<'\t'<<(fabs(P-Ps[m])/P)*100<<"%"<<'\n';
		output<<endl;
	}
	output.close();

	cin.get();
	return 0;
}

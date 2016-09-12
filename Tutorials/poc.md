#1 - Requirements and Specifications
Gather business requirements and specifications. Talk to the domain experts and try to learn as much as possible

#2 - Analyze the results from step 1
- in which bounded context the feature fits in?
- do we break boundaries in some way?
- is it a generig BC?

##2.1 - Small effort
Analysis outcome is that the feature can be done in 1 or 2 sprints. GOTO => 5

##2.1 - Big effort
Analysis outcome is that it is a big feature or probably a new bounded context. GOTO => 3

#3 - Develop POC
Develop a technical solution as fast and dirty as possible just to see if it will work

#4 - Present POC to the team
- when POC is ready present it to the rest of the team
- if the team approves it just continue with the rest of the steps
- if the team does not approve it GOTO => 3

#5 - Estimate
Estimate the rest of the work for integrating the POC into the working solution

#6 - Present the estimated POC to the steak holders and the domain experts
- if approved GOTO => 7
- if not approved GOTO => 1

#7 - Integrate POC

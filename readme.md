Set up the following SSM parameters:

aws ssm put-parameter --name /build-number/demo-build --type String --value "1"

aws ssm put-parameter --name /github/personal_access_token --type String --value "<SPECIFY GITHUB PERSONAL ACCESS TOKEN>"

aws ssm put-parameter --name /Demo/DBUser --type String --value "<SPECIFY USERNAME>"

aws ssm put-parameter --name /Demo/DBPassword --type String --value "<SPECIFY PASSWORD>"

Execute the following command via the AWS CLI, replacing the parameters as appropriate:

aws cloudformation create-stack --stack-name demo-api-pipeline --template-url https://s3.ap-southeast-2.amazonaws.com/demo-api-pipeline-template/pipeline.yaml --parameters ParameterKey=PipelineName,ParameterValue=demoapi ParameterKey=RepositoryOwner,ParameterValue=stuartraines ParameterKey=RepositoryName,ParameterValue=lunch-learn ParameterKey=RepositoryBranch,ParameterValue=master --capabilities=CAPABILITY_IAM

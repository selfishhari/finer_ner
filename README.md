# finer_ner

This repo has code to 2 Parts (train model & run pipeline in C# using ML.Net)
1. preproc finder139 dataset, and train a distill bert model on 4/5 top classes of it
  1. 02_data_prep.ipynb preps data, aligns mis match between bert wordpiece and provided tokens, subsamples data as well
  2. 03_train trains the trains a DistillBert model and pushes it to huggingface https://huggingface.co/HariLuru/finer_distillbert_v2. Also writes to ONNX
  3. 04_evaluation_v2 runs the model on test data and reports accuracy & latency numbers. ALso provides confusion matrix and error analysis
  4. ![image](https://github.com/selfishhari/finer_ner/assets/51013293/e441eb99-b05c-426d-b739-422542f8976f)
  5. ![image](https://github.com/selfishhari/finer_ner/assets/51013293/ae4b85e1-af83-4474-b6ec-19f0e8eb2d4d)

Try on hugging face:
https://huggingface.co/HariLuru/finer_distillbert_v2

![image](https://github.com/selfishhari/finer_ner/assets/51013293/b98c7bde-bfba-4ec1-9d57-55dc0a7dcdc1)



2. A C# program that runs on ML.net + ONNX runtime to read from a text file, and predict 5 XBRL tags
       go to mlnet/NamedEntityRecognizer and follow the readme


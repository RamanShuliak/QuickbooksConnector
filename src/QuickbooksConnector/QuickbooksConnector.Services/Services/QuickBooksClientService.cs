﻿using QBXMLRP2Lib;
using QuickbooksConnector.Services.Configurations;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;

namespace QuickbooksConnector.Services.Services;

public interface IQuickBooksClientService
{
    ValueTask<string> SendRequestToQuickBooksAsync(string qbxmlRequest);
}

public class QuickBooksClientService : IQuickBooksClientService
{
    private readonly RequestProcessor2 _requestProcessor;
    private readonly QuickBooksConfig _quickBooksConfig;

    public QuickBooksClientService(
            IOptions<QuickBooksConfig> quickBooksConfig)
    {
        if (quickBooksConfig == null
            || quickBooksConfig.Value.AppId == default
            || quickBooksConfig.Value.AppName == default)
        {
            throw new ArgumentException(
                "QuickBooks configuration is not provided or is invalid.",
                nameof(quickBooksConfig));
        }

        _requestProcessor = new RequestProcessor2();
        _quickBooksConfig = quickBooksConfig.Value;
    }

    public ValueTask<string> SendRequestToQuickBooksAsync(string qbxmlRequest)
    {
        try
        {
            _requestProcessor.OpenConnection(
                _quickBooksConfig.AppId, 
                _quickBooksConfig.AppName);

            string ticket = _requestProcessor.BeginSession("", QBFileMode.qbFileOpenDoNotCare);

            string qbxmlResponse = _requestProcessor.ProcessRequest(ticket, qbxmlRequest);

            _requestProcessor.EndSession(ticket);

            return new ValueTask<string>(qbxmlResponse);
        }
        catch (COMException ex)
        {
            throw new COMException("Error during connection process with QuickBooks through QBXMLRP2", ex);
        }
        finally
        {
            _requestProcessor.CloseConnection();
        }
    }
}
